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
                var token = await SecureStorage.GetAsync(AppConsts.Access_Token);
                var refren_token = await SecureStorage.GetAsync(AppConsts.Refresh_Token);
                await DisplayAlert("提示",retult.Length.ToString(),"取消");
            }
            catch(Exception ex)
            {
                if(ex is UnauthorizedAccessException)
                {
                    var token =await SecureStorage.GetAsync(AppConsts.Access_Token);
                    var refren_token = await SecureStorage.GetAsync(AppConsts.Refresh_Token);
                    if (token == null || refren_token == null)
                        return;
                    var result=await AppServices.Current.Services.GetService<IAccountService>().RefreshToken(token,refren_token);
                }
            }
        }
    }
}