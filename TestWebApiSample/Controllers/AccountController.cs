﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestWebApiSample.Entity;
using TestWebApiSample.ViewModel;

namespace TestWebApiSample.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _Logger = logger;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<TokeResult> Login(LoginViewModel loginViewModel)
        {
            var cache = HttpContext.RequestServices.GetService<IDistributedCache>();
            if (loginViewModel == null)
                throw new ArgumentNullException(nameof(loginViewModel));
            var exesitUser=_UserManager.Users.FirstOrDefault(t => t.UserName == loginViewModel.UserName);
            if (exesitUser == null)
                throw new ArgumentException("该用户不存在");
            var _token = await cache.GetAsync(CacheConsts.Access_Token + "__" + loginViewModel.UserName);
            var _access_token = await cache.GetAsync(CacheConsts.Refresh_Token + "__" + loginViewModel.UserName);
            if (_token != null&& _access_token!=null)
            {
                var access_token = Encoding.UTF8.GetString(_token);
                var reresh_token = Encoding.UTF8.GetString(_access_token);
                return new TokeResult() { Token = access_token, RefreshToken = reresh_token, Succeeded = true, Error = string.Empty };
            }
            else
            {
                var result = await _SignInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.PassWord, false, false);
                if (result.Succeeded)
                {
                    var user = await _UserManager.Users.SingleOrDefaultAsync(t => t.UserName == loginViewModel.UserName);
                    var token = GetAssceeToken(user);
                    await cache.SetAsync(CacheConsts.Access_Token + "__" + user.UserName, System.Text.Encoding.UTF8.GetBytes(token), new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = new TimeSpan(DateTime.Now.AddMinutes(120).Ticks) });
                    var _re_token = await GetRefreshToken();
                    await cache.SetAsync(CacheConsts.Refresh_Token + "__" + user.UserName, System.Text.Encoding.UTF8.GetBytes(_re_token), new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = new TimeSpan(DateTime.Now.AddDays(7).Ticks) });
                    return new TokeResult() { Token = token, RefreshToken =_re_token, Succeeded = true, Error = string.Empty };
                }
                else
                    return new TokeResult() { Token = "", Succeeded = false, Error = string.Empty };
            }
            
        }
        private string GetAssceeToken(User user)
        {
            var config = HttpContext.RequestServices.GetService<IConfiguration>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Issuer"], null, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<string> GetRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rand = RandomNumberGenerator.Create())
            {
                rand.GetBytes(randomNumber);
                return await Task.FromResult(Convert.ToBase64String(randomNumber));
            }
        }
        public ClaimsPrincipal GetPrincipal(string token)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(token);
            if (jwtSecurityToken == null)
                return null;
            var config = HttpContext.RequestServices.GetService<IConfiguration>();
            byte[] key = Encoding.UTF8.GetBytes(config["Jwt:Key"]); //此处编码方式需要和生成token编码方式一致
            var parameters = new TokenValidationParameters
            {
                RequireAudience = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
            SecurityToken securityToken;
            ClaimsPrincipal principal = jwtSecurityTokenHandler.ValidateToken(token, parameters, out securityToken);
            return principal;
        }
        public async Task<string> InitUser()
        {

            try
            {
                var roleManager = HttpContext.RequestServices.GetService<RoleManager<Role>>();
                if (!roleManager.Roles.Any())
                {
                    var role = new Role() { Name = "admin" };
                    _Logger.LogInformation($"初始化角色{role.Name}");
                    await roleManager.CreateAsync(role);
                }
                if (!_UserManager.Users.Any())
                {
                    var user = new User() { Email = "ui@test.com", UserName = "ui@test.com", SecurityStamp = Guid.NewGuid().ToString() };
                    _Logger.LogInformation($"初始化用户{user.UserName}");

                    await _UserManager.CreateAsync(user, "123@Abc");
                    await _UserManager.AddToRoleAsync(user, "admin");
                }
                return "isOk";
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Failed To Crete Default User or Role /n");
                _Logger.LogError(ex.Message.ToString());
                return "Failed";
            }
        }
    }
}
