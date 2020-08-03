using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using UI.Mobie.BasicCore;
using Xamarin.Forms;

namespace UI.Mobie.AppCore.Services
{
    public class AppAuthenticationManager : IAppAuthenticationManager
    {
        public Application Application { get; }
        public AppAuthenticationManager(Application application)
        {
            Application = application;
        }
        public void Login(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
                throw new ArgumentNullException(nameof(claimsPrincipal));
            AppServices.Current.User.AddIdentities(claimsPrincipal.Identities);
        }

        public void LogOut()
        {
            ((AppContext)AppServices.Current).User = new ClaimsPrincipal();
        }
    }
}
