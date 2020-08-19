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
            //var client = AppHttpClient.Current.CreateHttpClient();
            //var token = await SecureStorage.GetAsync("AuthToken");
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer "+token);
            //var result = await client.GetAsync("http://10.67.2.59/api/WeatherForecast/GetValues");
            //if (result.IsSuccessStatusCode)
            //{
            //    var data = await result.Content.ReadAsStringAsync();
            //    var _data = JsonConvert.DeserializeObject<WeatherViewModel[]>(data);
            //    Console.WriteLine(data);
            //}
            try
            {
                var retult = await AppServices.Current.Services.GetService<IWeatherService>().GetWeathers();

            }
            catch(Exception ex)
            {

            }
        }
    }
}