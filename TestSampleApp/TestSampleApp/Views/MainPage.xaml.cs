using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Interface;
using TestSampleApp.ViewModels;
using UI.Mobie.AppCore;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestSampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void TestButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var retult = await AppServices.Current.Services.GetService<IWeatherService>().GetWeathers();
            }
            catch(Exception ex)
            {
                if(ex is UnauthorizedAccessException)
                {

                }
            }
        }
    }
}