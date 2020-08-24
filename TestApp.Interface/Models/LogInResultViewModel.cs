using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Interface.Models
{
    public class LogInResultViewModel
    {
        public bool Succeeded { get; set; }
        public string RefreshToken { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
    }
}
