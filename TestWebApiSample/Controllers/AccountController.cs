using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApiSample.Entity;
using TestWebApiSample.ViewModel;

namespace TestWebApiSample.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;
        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _Logger = logger;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<TokeResult> Login([FromBody]LoginViewModel loginViewModel)
        {
            if (loginViewModel == null)
                throw new ArgumentNullException(nameof(loginViewModel));
            var result = await _SignInManager.PasswordSignInAsync(loginViewModel.UserName,loginViewModel.PassWord,false,false);
            if (result.Succeeded)
            {
                var user =await _UserManager.Users.SingleOrDefaultAsync(t=>t.UserName==loginViewModel.UserName);
                var token= GetAssceeToken(user);
                return new TokeResult() { Token = token, Succeeded = true, Error = string.Empty };
            }
            else
                return new TokeResult() { Succeeded = result.Succeeded,Token=string.Empty,Error="登录失败，请验证账号密码"};
        }
        private string GetAssceeToken(User user)
        {
            var config = HttpContext.RequestServices.GetService<IConfiguration>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials= new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(config["Jwt:Issuer"],config["Jwt:Issuer"],null,expires:DateTime.Now.AddMinutes(30),signingCredentials:credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
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
                    var user = new User() { Email = "ui@test.com", UserName = "ui@test.com",SecurityStamp = Guid.NewGuid().ToString()};
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
