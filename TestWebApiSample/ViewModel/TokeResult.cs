using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApiSample.ViewModel
{
    public class TokeResult
    {
        public int StatusCode { get; set; }
        public bool Succeeded { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Error { get; set; }
    }
}
