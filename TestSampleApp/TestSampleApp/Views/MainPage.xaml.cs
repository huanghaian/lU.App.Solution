﻿using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Interface;
using TestApp.Interface.Models;
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
                //var result =await AppServices.Current.Services.GetRequiredService<IWeatherService>().GetWeathers();
                //await DisplayAlert("提示", result.Length.ToString(), "取消");
                using (var client = new HttpClient())
                {
                    var dic = new Dictionary<string, string>() { { "accessToken", "accessToken" }, { "refreshAccessToken", "refreshAccessToken" } };
                    var data = JsonConvert.SerializeObject(dic);
                    var condent = new StringContent(data, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("http://localhost:8080/api/account/RefreshAccessToken", condent);
                    var model = JsonConvert.DeserializeObject<LogInResultViewModel>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                if (ex is UnauthorizedAccessException)
                {
                    var token = await SecureStorage.GetAsync(AppConsts.Access_Token);
                    var refren_token = await SecureStorage.GetAsync(AppConsts.Refresh_Token);
                    if (token == null || refren_token == null)
                        return;
                    var content = await AppServices.Current.Services.GetRequiredService<IAccountService>().RefreshToken(token, refren_token);
                    if (content==null||!content.Succeeded)
                    {
                        SecureStorage.Remove(AppConsts.Access_Token);
                        SecureStorage.Remove(AppConsts.Refresh_Token);
                        await DisplayAlert("提示", "已失效，请重新登录。", "确定");
                        Navigation.InsertPageBefore(new LoginPage(), this);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await SecureStorage.SetAsync(AppConsts.Access_Token, content.Token);
                        await SecureStorage.SetAsync(AppConsts.Refresh_Token, content.RefreshToken);
                    }
                }
                //}
            }
        }
    }
}