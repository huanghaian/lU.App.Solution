using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace UI.Mobie.BasicCore
{
    public interface IAppAuthenticationManager
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        void Login(ClaimsPrincipal claimsPrincipal);
        /// <summary>
        /// 登出
        /// </summary>
        void LogOut();
    }
}
