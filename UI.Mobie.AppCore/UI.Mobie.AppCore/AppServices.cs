using System;
using System.Collections.Generic;
using System.Text;
using UI.Mobie.BasicCore;
using Xamarin.Forms;

namespace UI.Mobie.AppCore
{
    public static class AppServices
    {
        public static IAppContext Current
        {
            get
            {
                var startup = Application.Current as AppStartup;
                if (startup == null)
                    return null;
                return startup.AppContext;
            }
        }
    }
}
