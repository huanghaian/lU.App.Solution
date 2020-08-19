using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Interface.Models;

namespace TestApp.Interface
{
    public interface IWeatherService
    {
        Task<WeatherViewModel[]> GetWeathers();
    }
}
