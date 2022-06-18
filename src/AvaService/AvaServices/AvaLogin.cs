using System.Net.Http.Headers;
using System.Text;

namespace Sample.AvaServices
{
    public static class AvaLogin
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task<HttpResponseMessage> getToken(string encryptedUserPass)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsync(Config.loginUrl+"mobileLogin",
                                                  new StringContent(@"{""acceptorCode"":"""+Config.acceptorCode +@""",
                                                  ""clientAddress"":"""+Config.clientAddress+@""",
                                                  ""encryptedCredentials"":"""
                                                  + encryptedUserPass + "\"}",
                                                  Encoding.UTF8,
                                                  "application/json"));            
            return response;
        }
    }
}
