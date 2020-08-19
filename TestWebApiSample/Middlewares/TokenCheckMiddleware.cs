using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApiSample.Middlewares
{
    public class TokenCheckMiddleware
    {
        RequestDelegate _next;
        IConfiguration _configuration;
        public TokenCheckMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"];
        }
    }
}
