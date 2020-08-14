using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TestSampleApp.Models;
using UI.Mobie.AppCore;
using Microsoft.AspNetCore.Http.Connections.Client;
using UI.Mobie.AppCore.Abstractions;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using TestSampleApp.ViewModels;

namespace TestSampleApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            try
            {
                var httpMessageHandlerFactory = DependencyService.Get<IHttpMessageHandlerFactory>();

                Action<HttpConnectionOptions> connectionOption = Options =>
                {
                    Options.HttpMessageHandlerFactory = messageHandler => httpMessageHandlerFactory.Handle(messageHandler, Options);
                };
                var httpclient = AppHttpClient.Current.CreateHttpClient(connectionOption);
                var json = "{\"username\":\"ui@test.com\",\"password\":\"123@Abc\"}";
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var result = await httpclient.PostAsync("http://0.0.0.0/api/account/login", content);
                if (result.IsSuccessStatusCode)
                {
                    var contentString = await result.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<LogInResultViewModel>(contentString);
                    Console.WriteLine(contentString);
                }
            }
            catch (Exception ex)
            {

            }

            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}