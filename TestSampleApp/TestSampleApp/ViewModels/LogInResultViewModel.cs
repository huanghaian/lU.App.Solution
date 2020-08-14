using System;
using System.Collections.Generic;
using System.Text;

namespace TestSampleApp.ViewModels
{
    public class LogInResultViewModel
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
    }
}
