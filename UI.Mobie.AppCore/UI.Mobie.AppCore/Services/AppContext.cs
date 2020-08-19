using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using UI.Mobie.BasicCore;

namespace UI.Mobie.AppCore.Services
{
    public class AppContext : IAppContext
    {
        private readonly IServiceProvider _Service;
        public AppContext(IServiceProvider serviceProvider)
        {
            _Service = serviceProvider;
            //Authentication = serviceProvider.GetRequiredService<IAppAuthenticationManager>();
            Settings = serviceProvider.GetRequiredService<IAppSetting>();
            User = new ClaimsPrincipal();
            User.AddIdentity(new ClaimsIdentity());
            ClaimsPrincipal.ClaimsPrincipalSelector = () => User;
            ClaimsPrincipal.PrimaryIdentitySelector = identies =>
            {
                ClaimsIdentity anonymous = null;
                return anonymous;
            };
        }
        public IAppAuthenticationManager Authentication { get; }

        public IServiceProvider Services { get => _Service; }

        public ClaimsPrincipal User { get; internal set; }

        public IAppSetting Settings { get; }
    }
}
