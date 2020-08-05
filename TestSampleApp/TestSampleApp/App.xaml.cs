using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestSampleApp.Services;
using TestSampleApp.Views;
using UI.Mobie.AppCore;
using Microsoft.Extensions.DependencyInjection;
using UI.Mobie.BasicCore.Services;
using UI.Mobie.AppCore.Services;

namespace TestSampleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class App : AppStartup
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }
        protected override void ConfigureServicesCore(IServiceCollection services)
        {
            services.AddSingleton(typeof(IRepositoryFactory),typeof(AppRepositoryFactory));
            services.AddSingleton(typeof(IRepository<>),typeof(Repository<>));
        }

    }
}
