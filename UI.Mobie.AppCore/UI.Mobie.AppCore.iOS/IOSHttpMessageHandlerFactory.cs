using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Foundation;
using Microsoft.AspNetCore.Http.Connections.Client;
using UI.Mobie.AppCore.Abstractions;
using UIKit;

namespace UI.Mobie.AppCore.iOS
{
    public class IOSHttpMessageHandlerFactory : IHttpMessageHandlerFactory
    {
        public HttpMessageHandler Handle(HttpMessageHandler messageHandler, HttpConnectionOptions options)
        {
            var handler = new NSUrlSessionHandler();
            handler.Credentials = ((HttpClientHandler)messageHandler).Credentials;
            handler.DisableCaching = true;
            return handler;
        }
    }
}