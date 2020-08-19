using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestSampleApp.Services;
using TestSampleApp.Views;
using UI.Mobie.AppCore;
using Microsoft.Extensions.DependencyInjection;
using UI.Mobie.AppCore.Services;
using UI.Mobie.AppCore.Abstractions;

namespace TestSampleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class App : AppStartup
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new LoginPage());
        }
        protected override void OnStartBase()
        {
            var httpMessageHandlerFactory = DependencyService.Get<IHttpMessageHandlerFactory>();
            AppHttpClient.Current.Options = Options =>
            {
                Options.HttpMessageHandlerFactory = messageHandler => httpMessageHandlerFactory.Handle(messageHandler, Options);
            };
        }
        protected override void ConfigureServicesCore(IServiceCollection services)
        {
            
        }

    }
}
