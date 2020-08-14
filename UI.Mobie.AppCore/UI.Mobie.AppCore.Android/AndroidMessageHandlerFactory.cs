using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.AspNetCore.Http.Connections.Client;
using UI.Mobie.AppCore.Abstractions;
using Xamarin.Android.Net;

namespace UI.Mobie.AppCore.Droid
{
    public class AndroidMessageHandlerFactory : IHttpMessageHandlerFactory
    {
        public HttpMessageHandler Handle(HttpMessageHandler messageHandler, HttpConnectionOptions options)
        {
            var handler = new AndroidClientHandler();
            handler.Credentials = ((HttpClientHandler)messageHandler).Credentials;
            return handler;
        }
    }
}