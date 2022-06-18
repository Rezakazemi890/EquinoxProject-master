using System.Net.Http.Headers;
using System.Text;

namespace Sample.AvaServices
{
    public class AvaDeposit
    {
        private static readonly HttpClient client2 = new HttpClient();
        public static async Task<HttpResponseMessage> getDepositBalance(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client2.PostAsync(Config.depositUrl + "getDepositBalance",
                                                  new StringContent(@"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""depositNumber"":""" + Config.depositNumber + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"}",
                                                  Encoding.UTF8,
                                                  "application/json"));
            
            return response;
        }

        public static async Task<HttpResponseMessage> getDeposits(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client2.PostAsync(Config.depositUrl + "getDeposits",
                                                  new StringContent(@"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""depositNumber"":""" + Config.depositNumber + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""length"":""" + 10 + "\"}",
                                                  Encoding.UTF8,
                                                  "application/json"));
            return response;
        }

        public static async Task<HttpResponseMessage> PolTransfer(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""sourceDescription"":""" + Config.sourceDescription + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""amount"":""" + Config.amount + @"""
                                                  ,""destinationIBAN"":""" + Config.destinationIBAN + @"""                                                  
                                                  ,""destinationIbanOwnerName"":""" + Config.destinationIbanOwnerName + @"""
                                                  ,""reasonCode"":""" + Config.reasonCode + @"""
                                                  ,""reasonTitle"":""" + Config.reasonTitle + @"""
                                                  ,""sourceDeposit"":""" + Config.sourceDeposit + "\"}";

            var response = await client2.PostAsync(Config.depositUrl + "instantTransfer",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));
            return response;
        }

        public static async Task<string> normalTransferOtp(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""sourceDescription"":""" + Config.sourceDescription + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""amount"":""" + Config.amount + @"""
                                                  ,""sourceDeposit"":""" + Config.sourceDeposit + @"""
                                                  ,""destinationDeposit"":""" + Config.destinationDeposit + "\"}";

            var response = await client2.PostAsync(Config.depositUrl + "normalTransferOtp",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public static async Task<string> achNormalTransferOtp(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""amount"":""" + Config.amount + @"""
                                                  ,""sourceDeposit"":""" + Config.sourceDeposit + @"""
                                                  ,""ibanNumber"":""" + Config.destinationIBAN + "\"}";

            var response = await client2.PostAsync(Config.depositUrl + "achNormalTransferOtp",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public static async Task<string> getCardsByDepositThirdParty(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""depositNumber"":""" + Config.depositNumber + @"""
                                                  ,""length"":""" + 10 + "\"}";

            var response = await client2.PostAsync(Config.cardUrl + "getCardsByDepositThirdParty",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public static async Task<string> getCardBalance(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""pan"":""" + Config.pan + @"""
                                                  ,""encryptedCredentials"":""" + Program.Encrypt(Config.cvv2 + "|" + Config.expDate + "|" + Config.pin + "|") + "\"}";

            var response = await client2.PostAsync(Config.cardUrl + "getCardBalance",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public static async Task<string> harimOtp(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""pan"":""" + Config.pan + "\"}";

            var response = await client2.PostAsync(Config.loginUrl + "harimOtp",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public static async Task<string> getDepositStatement(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""depositNumber"":""" + Config.depositNumber + @"""
                                                  ,""fromDate"":""" + Config.fromDate + @"""
                                                  ,""length"":""" + 10 + @"""
                                                  ,""toDate"":""" + Config.toDate + "\"}";

            var response = await client2.PostAsync(Config.depositUrl + "getDepositStatement",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public static async Task<string> normalTransferWithThirdParty(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""amount"":""" + Config.amount + @"""
                                                  ,""sourceDeposit"":""" + Config.sourceDeposit + @"""
                                                  ,""destinationDeposit"":""" + Config.destinationDeposit + @"""
                                                  ,""sourceComment"":""" + "Test" + "\"}";

            var response = await client2.PostAsync(Config.depositUrl + "normalTransferWithThirdParty",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
