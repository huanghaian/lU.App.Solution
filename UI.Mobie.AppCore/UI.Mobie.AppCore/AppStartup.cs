using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UI.Mobie.AppCore
{
    public class AppStartup:Application
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(this);
            ConfigureServicesCore(services);
        }

        protected virtual void ConfigureServicesCore(IServiceCollection services)
        {

        }

        protected sealed override void OnStart()
        {
            OnStartBase();
        }

        protected sealed override void OnSleep()
        {
            OnSleepBase();
        }
        protected sealed override void OnResume()
        {
            OnResumeBase();
        }
        protected virtual void OnSleepBase()
        {
        }

        protected virtual void OnStartBase()
        {

        }
        protected virtual void OnResumeBase()
        {

        }
    }
}
