using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Interface.Models;

namespace TestApp.Interface
{
    public interface IAccountService
    {
        Task<LogInResultViewModel> Login(string user,string pwd);
        Task<LogInResultViewModel> RefreshToken(string token,string refreshToken);
    }
}
