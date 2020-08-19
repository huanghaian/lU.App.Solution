using Microsoft.AspNetCore.Http.Connections.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UI.Mobie.AppCore.Abstractions;
using Xamarin.Forms;

namespace TestSampleApp
{
    public class AppHttpClient
    {
        private static IHttpMessageHandlerFactory httpMessageHandlerFactory;
        private static AppHttpClient _AppHttpClient;
        static AppHttpClient()
        {
            _AppHttpClient = new AppHttpClient();
        }

        public Action<HttpConnectionOptions> Options { get; set; }

        public static AppHttpClient Current { get => _AppHttpClient; }
        private AppHttpClient()
        {

        }
        public HttpClient CreateHttpClient(Action<HttpConnectionOptions> connectionOption=null)
        { 
            var opt = new HttpConnectionOptions();
            Options?.Invoke(opt);
            var handler = new HttpClientHandler();
            handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            var client = new HttpClient(opt.HttpMessageHandlerFactory(handler),false);
            return client;
        }
    }
}
