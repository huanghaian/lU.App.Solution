using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestSampleApp.Attributes
{
    public class ServerAttribute:Attribute
    {
        private string _api;
        public ServerAttribute(string api)
        {
            _api = api;
        }
        public string Api { get => _api; }
    }
}
