using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace UI.Mobie.BasicCore
{
    public interface IAppContext
    {
        IAppAuthenticationManager Authentication { get; }
        IServiceProvider Services { get; }
        ClaimsPrincipal User { get; }
        IAppSetting Settings { get; }
    }
}
