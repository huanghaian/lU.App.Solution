using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Interface;

namespace TestApp.Interface.Models
{
    public class WeatherViewModel
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string Summary { get; set; }
    }
}
