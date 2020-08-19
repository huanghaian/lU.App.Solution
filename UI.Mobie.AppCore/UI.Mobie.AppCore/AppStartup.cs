using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Mobie.AppCore.Services;
using UI.Mobie.BasicCore;
using Xamarin.Forms;

namespace UI.Mobie.AppCore
{
    public class AppStartup:Application
    {
        public Services.AppContext AppContext { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Application>(this);
            services.AddSingleton(this);
            services.AddSingleton<IAppStorageProvider, AppStorageProvider>();
            ConfigureServicesCore(services);
        }

        protected virtual void ConfigureServicesCore(IServiceCollection services)
        {

        }
        public void Configure(IServiceProvider serviceProvider)
        {
            AppContext = new Services.AppContext(serviceProvider);
            ConfigureCore(serviceProvider);
        }
        protected virtual void ConfigureCore(IServiceProvider serviceProvider) { }

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
