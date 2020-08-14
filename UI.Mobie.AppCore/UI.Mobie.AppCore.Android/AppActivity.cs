using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms.Platform.Android;
using UI.Mobie.BasicCore;
using Xamarin.Forms;
using UI.Mobie.AppCore.Abstractions;

namespace UI.Mobie.AppCore.Droid
{
    public class AppActivity<T> :FormsAppCompatActivity where T:AppStartup,new()
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            OnPreInitForms();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            OnInitForms();
            DependencyService.Register<IHttpMessageHandlerFactory, AndroidMessageHandlerFactory>();
            var services = CreateServiceContainer();
            services.AddSingleton<Activity>(this);
            services.AddSingleton(ApplicationContext);
            services.AddSingleton<IAppSetting, AppSettings>();
            var app = new T();
            app.ConfigureServices(services);
            ConfigureServices(services);
            var serviceProvider= services.BuildServiceProvider();
            app.Configure(serviceProvider);
            LoadApplication(app);
        }
        protected virtual void ConfigureServices(IServiceCollection services) { }

        protected virtual void OnPreInitForms()
        {

        }

        protected virtual void OnInitForms()
        {

        }

        protected virtual IServiceCollection CreateServiceContainer()
        {
            return new ServiceCollection();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}