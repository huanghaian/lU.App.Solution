using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UI.Mobie.AppCore
{
    public class AppStartup:Application
    {
        protected sealed override void OnStart()
        {
            OnStartBase();
        }

        protected sealed override void OnSleep()
        {
            OnSleepBase();
        }

        protected virtual void OnSleepBase()
        {
        }

        protected virtual void OnStartBase()
        {

        }
    }
}
