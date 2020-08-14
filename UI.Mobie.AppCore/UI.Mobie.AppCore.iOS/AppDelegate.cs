using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.Extensions.DependencyInjection;
using UI.Mobie.AppCore.Abstractions;
using UI.Mobie.AppCore.iOS.services;
using UI.Mobie.BasicCore;
using UIKit;
using Xamarin.Forms;

[assembly: Preserve(typeof(System.Linq.Queryable), AllMembers = true)]
namespace UI.Mobie.AppCore.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    public partial class AppDelegateBase<T> : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate where T:AppStartup,new()
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            OnPreInitForms();
            global::Xamarin.Forms.Forms.Init();
            OnInitForms();
            DependencyService.Register<IHttpMessageHandlerFactory, IOSHttpMessageHandlerFactory>();
            var services = CreateServiceContainer();
            services.AddSingleton(app);
            services.AddSingleton<IAppSetting, AppSettings>();
            var appStart = new T();
            appStart.ConfigureServices(services);
            ConfigureServices(services);
#pragma warning disable CS1702 // 假定程序集引用与标识匹配
            var serviceProvider = services.BuildServiceProvider();
#pragma warning restore CS1702 // 假定程序集引用与标识匹配
            appStart.Configure(serviceProvider);

            LoadApplication(appStart);

            return base.FinishedLaunching(app, options);
        }

        protected virtual IServiceCollection CreateServiceContainer()
        {
            return new ServiceCollection();
        }
        protected virtual void ConfigureServices(IServiceCollection services) { }

        protected virtual void OnInitForms()
        {
        }

        protected virtual void OnPreInitForms()
        {
        }
    }
}
