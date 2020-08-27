using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestSampleApp.Services;
using TestSampleApp.Views;
using UI.Mobie.AppCore;
using UI.Mobie.AppCore.Services;
using UI.Mobie.AppCore.Abstractions;
using TestApp.Interface;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

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

            var account = AsyncHelper.RunWithResultAsync(async () => { return await SecureStorage.GetAsync(AppConsts.User_Account); });
            if (account != null)
            {
                var result = JsonConvert.DeserializeObject<Dictionary<string,string>>(account);
                _ = Task.Run(async() =>
                {
                   var model =await AppServices.Current.Services.GetRequiredService<IAccountService>().Login(result["username"],result["password"]) ;
                    if (!model.Succeeded)
                        return;
                }).ContinueWith(task=> {
                    if (task.IsFaulted)
                        return;
                    MainPage = new NavigationPage(new MainPage());
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }
        protected override void OnStartBase()
        {
            
        }
        protected override void ConfigureServicesCore(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<IAccountService, AccountService>();
        }

    }
}
