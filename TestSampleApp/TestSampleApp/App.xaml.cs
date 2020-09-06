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
using System.Net.Http;
using TestApp.Interface.Models;

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
            AppHttpClient.BaiseUrl = "http://localhost";
            DependencyService.Register<MockDataStore>();

            var account = AsyncHelper.RunWithResultAsync(async () => { return await SecureStorage.GetAsync(AppConsts.User_Account); });
            if (account != null)
            {
                var result = JsonConvert.DeserializeObject<Dictionary<string,string>>(account);
                var loginResult = AsyncHelper.RunWithResultAsync(async()=>
                {
                    var httpclient = AppHttpClient.Current.CreateHttpClient();
                    var data = new Dictionary<string, string>() { { "username", result["username"] }, { "password", result["password"] } };
                    var jsonData = JsonConvert.SerializeObject(data);
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    var reposeContent = await httpclient.PostAsync("/api/account/login", content);
                    if (reposeContent.IsSuccessStatusCode)
                    {
                        var contentString = await reposeContent.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<LogInResultViewModel>(contentString);
                        return model;
                    }
                    else
                        return new LogInResultViewModel() { Error = "请求错误。" };

                });
                if (loginResult.Succeeded)
                {

                    MainPage = new NavigationPage(new MainPage());
                    Task.Run(async() =>
                    {
                        await SecureStorage.SetAsync(AppConsts.Access_Token, loginResult.Token);
                        await SecureStorage.SetAsync(AppConsts.Refresh_Token, loginResult.RefreshToken);
                    });
                    
                }
                else
                    MainPage = new NavigationPage(new LoginPage()); 
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
