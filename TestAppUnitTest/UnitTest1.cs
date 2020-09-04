using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestWebApiSample.ViewModel;

namespace TestAppUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            using (var client = new HttpClient())
            {
                var dic = new Dictionary<string, string>() { { "accessToken", "accessToken" }, { "refreshAccessToken", "refreshAccessToken" } };
                var condent = new FormUrlEncodedContent(dic);
                var result = await client.PostAsync("http://10.67.2.41/api/account/RefreshAccessToken", condent);
                var model =JsonConvert.DeserializeObject<TokeResult>(await result.Content.ReadAsStringAsync());
                Assert.IsTrue(model.Succeeded);
            }
        }
    }
}
