using Microsoft.AspNetCore.Http.Connections.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace UI.Mobie.AppCore.Abstractions
{
    public interface IHttpMessageHandlerFactory
    {
        HttpMessageHandler Handle(HttpMessageHandler messageHandler, HttpConnectionOptions options);
    }
}
