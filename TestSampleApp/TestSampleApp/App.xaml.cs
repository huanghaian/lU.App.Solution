using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestSampleApp.Services;
using TestSampleApp.Views;
using UI.Mobie.AppCore;
using Microsoft.Extensions.DependencyInjection;
using UI.Mobie.AppCore.Services;
using UI.Mobie.AppCore.Abstractions;
using TestApp.Interface;
using Xamarin.Essentials;

namespace TestSampleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class App : AppStartup
    {

        public App()
        {
            InitializeComponent();
            var httpMessageHandlerFactory = DependencyService.Get<IHttpMessageHandlerFactory>();
            AppHttpClient.Current.Options = Options =>
            {
                Options.HttpMessageHandlerFactory = messageHandler => httpMessageHandlerFactory.Handle(messageHandler, Options);
            };
            AppHttpClient.BaiseUrl = "http://10.67.2.59";
            DependencyService.Register<MockDataStore>();

            var token = AsyncHelper.RunWithResultAsync(async () => { return await SecureStorage.GetAsync(AppConsts.Access_Token); });
            if (token != null)
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }
        protected override void OnStartBase()
        {
            
        }
        protected override void ConfigureServicesCore(IServiceCollection services)
        {
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<IAccountService, AccountService>();
        }

    }
}
