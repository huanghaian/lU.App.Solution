using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestApp.Interface;
using TestApp.Interface.Models;

namespace TestSampleApp.Services
{
    public class AccountService : IAccountService
    {
        HttpClient _client;
        public AccountService()
        {
            _client = AppHttpClient.Current.CreateHttpClient();
        }

        public Task<LogInResultViewModel> Login(string user, string pwd)
        {
            throw new NotImplementedException();
        }

        public async Task<LogInResultViewModel> RefreshToken(string token, string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentNullException(refreshToken);
            var dic = new Dictionary<string, string>() { { "accessToken", token }, { "refreshAccessToken", refreshToken } };
            var data = JsonConvert.SerializeObject(dic);
            var condent = new StringContent(data,Encoding.UTF8, "application/json");
            var result =await _client.GetAsync("/api/account/RefreshAccessToken?accessToken="+token+"&&"+ "refreshAccessToken="+refreshToken);
            var resposeResult = await result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<LogInResultViewModel>(resposeResult);
            return model;

        }
    }
}
