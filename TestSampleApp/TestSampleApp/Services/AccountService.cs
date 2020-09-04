using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestApp.Interface;
using TestApp.Interface.Models;
using Xamarin.Essentials;

namespace TestSampleApp.Services
{
    public class AccountService : IAccountService
    {
        HttpClient _client;
        public AccountService()
        {
            _client = AppHttpClient.Current.CreateHttpClient();
        }

        public async Task<LogInResultViewModel> Login(string user, string puserwd)
        {
            var data = new Dictionary<string, string>() { { "username", user }, { "password", user } };
            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/api/account/login", content);
            var contentString = await result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<LogInResultViewModel>(contentString);
            if (result.IsSuccessStatusCode)
            {
                if (model.Succeeded)
                {
                    _ = Task.Run(async () =>
                    {
                        await SecureStorage.SetAsync(AppConsts.Access_Token, model.Token);
                        await SecureStorage.SetAsync(AppConsts.Refresh_Token, model.RefreshToken);

                        await SecureStorage.SetAsync(AppConsts.User_Account, jsonData);
                    });
                }
                return model;

            }
            else
            {
                return new LogInResultViewModel() { Error = await result.Content.ReadAsStringAsync() };
            }
        }

        public async Task<LogInResultViewModel> RefreshToken(string token, string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentNullException(refreshToken);
            var dic = new Dictionary<string, string>() { { "accessToken", token }, { "refreshAccessToken", refreshToken } };
            //var data = JsonConvert.SerializeObject(dic);
            var condent = new FormUrlEncodedContent(dic);
            var uri= "/api/account/RefreshAccessToken?accessToken";
            var result =await _client.PostAsync(uri,condent);
            var resposeResult = await result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<LogInResultViewModel>(resposeResult);
            return model;
        }
    }
}
