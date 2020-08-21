using Microsoft.AspNetCore.Http.Connections.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestSampleApp.ViewModels;
using UI.Mobie.AppCore;
using UI.Mobie.AppCore.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestSampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private static bool IsLoaded = false;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private async void OnLoginButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await LoginCore(username.Text, password.Text);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task LoginCore(string userName, string Passwod)
        {
            var httpclient = AppHttpClient.Current.CreateHttpClient();
            var data = new Dictionary<string, string>() { { "username", userName }, { "password", Passwod } };
            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var result = await httpclient.PostAsync("/api/account/login", content);
            if (result.IsSuccessStatusCode)
            {
                var contentString = await result.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<LogInResultViewModel>(contentString);
                if (model.Succeeded)
                {
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                    _ = Task.Run(async () =>
                    {
                        await SecureStorage.SetAsync(AppConsts.Access_Token, model.Token);
                        await SecureStorage.SetAsync(AppConsts.User_Account, jsonData);
                    });
                }
                else
                {
                    await this.DisplayAlert("提示", "登录失败", "取消");
                }
            }
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            var result = await SecureStorage.GetAsync(AppConsts.User_Account);
            var account = JsonConvert.DeserializeObject<AccountViewModel>(result);
            if (account != null)
            {
                await LoginCore(account.UserName, account.PassWord);
            }
        }
    }
}