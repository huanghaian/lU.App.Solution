﻿using Microsoft.AspNetCore.Http.Connections.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using TestSampleApp.ViewModels;
using UI.Mobie.AppCore.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestSampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
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
                var httpclient = AppHttpClient.Current.CreateHttpClient();
                var data = new Dictionary<string, string>() { { "username", username.Text }, { "password", password.Text } };
                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                var result = await httpclient.PostAsync("http://*/api/account/login", content);
                if (result.IsSuccessStatusCode)
                {

                    var contentString = await result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<LogInResultViewModel>(contentString);
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                    _ = Task.Run(() =>
                      {
                          return SecureStorage.SetAsync("AuthToken", model.Token);
                      });


                }
            }
            catch (Exception ex)
            {
                if (ex is AuthenticationException)
                {
                    await this.DisplayAlert("提示", "登录失败", "取消");
                }
            }
        }
    }
}