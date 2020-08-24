using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TestApp.Interface;
using TestApp.Interface.Models;
using TestSampleApp.Attributes;
using Xamarin.Essentials;

namespace TestSampleApp.Services
{
    public class WeatherService : IWeatherService
    {
        HttpClient _client;
        public WeatherService()
        {
            _client = AppHttpClient.Current.CreateHttpClient();
            var token = AsyncHelper.RunAsync(async()=> { return await SecureStorage.GetAsync(AppConsts.Access_Token); });
            if (token == null)
                throw new ArgumentNullException(nameof(token));
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
        }
        public async Task<WeatherViewModel[]> GetWeathers()
        {
            if (_client == null)
                throw new ArgumentException(nameof(_client));
            var result =await _client.GetAsync("/api/WeatherForecast/GetValues");
            if (result.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<WeatherViewModel[]>(await result.Content.ReadAsStringAsync());
                return data;
            }else if(result.StatusCode== HttpStatusCode.Unauthorized||result.StatusCode==HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException(result.ReasonPhrase);
            }
            else
            {
                return new WeatherViewModel[0];
            }
        }
    }
}
