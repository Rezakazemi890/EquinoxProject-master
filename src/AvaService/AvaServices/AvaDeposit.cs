using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;

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
                                                  ,""reasonCode"":""" + await getAllAchTransactionReasonCode(await getAllAchTransactionReason(string.Empty)) + @"""
                                                  ,""reasonTitle"":""" + await getAllAchTransactionReasonTitle(await getAllAchTransactionReason(string.Empty)) + @"""
                                                  ,""sourceDeposit"":""" + Config.sourceDeposit + "\"}";

            var response = await client2.PostAsync(Config.depositUrl + "instantTransfer",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));
            return response;
        }

        public static async Task<HttpResponseMessage> normalTransferOtp(string token)
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

            return response;
        }

        public static async Task<HttpResponseMessage> achNormalTransferOtp(string token)
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

            return response;
        }

        public static async Task<HttpResponseMessage> getCardsByDepositThirdParty(string token)
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

            return response;
        }

        public static async Task<HttpResponseMessage> getCardBalance(string token)
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

            return response;
        }

        public static async Task<HttpResponseMessage> harimOtp(string token)
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

            return response;
        }

        public static async Task<HttpResponseMessage> getDepositStatement(string token)
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

            return response;
        }

        public static async Task<HttpResponseMessage> normalTransferWithThirdParty(string token)
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

            return response;
        }

        public static async Task<HttpResponseMessage> normalTransfer(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""encryptedCredentials"":""" + Program.Encrypt(Config.ticket + "|" + Config.secondPassword + "|") + @"""
                                                  ,""amount"":""" + Config.amount + @"""
                                                  ,""sourceDeposit"":""" + Config.sourceDeposit + @"""
                                                  ,""destinationDeposit"":""" + Config.destinationDeposit + @"""
                                                  ,""sourceComment"":""" + "Test" + "\"}";

            var response = await client2.PostAsync(Config.depositUrl + "normalTransfer",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> achNormalTransfer(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""encryptedCredentials"":""" + Program.Encrypt(Config.ticket + "|" + Config.secondPassword + "|") + @"""
                                                  ,""amount"":""" + Config.amount + @"""
                                                  ,""sourceDepositNumber"":""" + Config.sourceDeposit + @"""
                                                  ,""ibanNumber"":""" + Config.destinationIBAN + @"""
                                                  ,""ownerName"":""" + Config.destinationIbanOwnerName + @"""
                                                  ,""reasonCode"":""" + await getAllAchTransactionReasonCode(await getAllAchTransactionReason(string.Empty)) + @"""
                                                  ,""reasonTitle"":""" + await getAllAchTransactionReasonTitle(await getAllAchTransactionReason(string.Empty)) + @"""
                                                  ,""transferDescription"":""" + Config.sourceDescription + @"""
                                                  ,""description"":""" + "Test" + "\"}";

            var response = await client2.PostAsync(Config.depositUrl + "achNormalTransfer",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> cardToIban(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""pan"":""" + Config.pan + "\"}";

            var response = await client2.PostAsync(Config.generalUrl + "cardToIban",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> getDepositsByCard(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""pan"":""" + Config.pan + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"}";

            var response = await client2.PostAsync(Config.depositUrl + "getDepositsByCard",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> depositToIban(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""depositNumber"":""" + Config.depositNumber + "\"}";

            var response = await client2.PostAsync(Config.generalUrl + "depositToIban",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> changeTransactionSecondPassword(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""currentPassword"":""" + Config.pin + @"""
                                                  ,""newPassword"":""" + Config.secondPassword + "\"}";

            var response = await client2.PostAsync(Config.depositUrl + "changeTransactionSecondPassword",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> changeCardStaticPin(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @""" 
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""pan"":""" + Config.pan + @"""
                                                  ,""encryptedCredentials"":""" + Program.Encrypt(Config.cvv2 + "|" + Config.expDate + "|" + Config.pin + "|" + Config.secondPassword + "|") + "\"}";

            var response = await client2.PostAsync(Config.cardUrl + "changeCardStaticPin",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> changeCardSecondOtpStatus(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""                                                  
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""pan"":""" + Config.pan + @"""
                                                  ,""status"":""" + "ACTIVE" + "\"}";

            var response = await client2.PostAsync(Config.cardUrl + "changeCardSecondOtpStatus",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> generateCardPin(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""                                                  
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""pan"":""" + Config.pan + "\"}";

            var response = await client2.PostAsync(Config.cardUrl + "generateCardPin",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> hotCard(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""pan"":""" + Config.pan + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""                                                  
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""encryptedCredentials"":""" + Program.Encrypt(Config.cvv2 + "|" + Config.expDate + "|" + Config.pin + "|") + @"""
                                                  ,""reason"":""" + "Test" + "\"}";

            var response = await client2.PostAsync(Config.cardUrl + "hotCard",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));
            return response;
        }

        public static async Task<HttpResponseMessage> activateCard(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""                                                  
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""pan"":""" + Config.pan + "\"}";

            var response = await client2.PostAsync(Config.cardUrl + "activateCard",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> cardTransfer(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""encryptedCredentials"":""" + Program.Encrypt(Config.cvv2 + "|" + Config.pin + "|" + Config.amount + "|" + Config.destinationPAN + "|") + @"""
                                                  ,""trackingNumber"":""" + Config.trackingNumber + @"""                                                  
                                                  ,""sourcePAN"":""" + Config.pan + @"""                                                  
                                                  ,""expDate"":""" + Config.expDate + @"""                                                  
                                                  ,""approvalCode"":""" + await getApprovalCode(await cardHolderInquiry(token)) + "\"}";

            var response = await client2.PostAsync(Config.cardUrl + "cardTransfer",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> cardTransferOtp(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""destinationPAN"":""" + Config.destinationPAN + @"""                                                  
                                                  ,""pan"":""" + Config.pan + @"""                                                  
                                                  ,""amount"":""" + Config.amount + "\"}";

            var response = await client2.PostAsync(Config.cardUrl + "cardTransferOtp",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }
        public static async Task<HttpResponseMessage> payLoan(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""amount"":""" + Config.amount + @"""
                                                  ,""loanNumber"":""" + Config.loanNumber + @"""
                                                  ,""paymentMethod"":""" + "AUTO_GET_DEPOSIT" + @"""
                                                  ,""additionalDescription"":""" + "Test" + @"""
                                                  ,""customDepositNumber"":""" + "" + "\"}";

            var response = await client2.PostAsync(Config.loanUrl + "payLoan",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> getLoans(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""branchCode"":""" + Config.branchCode + @"""
                                                  ,""cbLoanNumber"":""" + Config.cbLoanNumber + @"""
                                                  ,""length"":""" + "1" + @"""
                                                  ,""offset"":""" + "1" + @"""
                                                  ,""loanStatus"":""" + "PAID_INCOMPLETE" + @"""
                                                  ,""loanTitle"":""" + Config.loanTitle + @"""
                                                  ,""type"":""" + "MOZAREBE" + "\"}";

            var response = await client2.PostAsync(Config.loanUrl + "getLoans",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> getLoanDetail(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""cbLoanNumber"":""" + Config.cbLoanNumber + @"""
                                                  ,""length"":""" + "1" + @"""
                                                  ,""offset"":""" + "1" + @"""
                                                  ,""loanNumber"":""" + Config.loanNumber + @"""
                                                  ,""payStatus"":""" + "PAID" + "\"}";

            var response = await client2.PostAsync(Config.loanUrl + "getLoanDetail",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> getAllAchTransactionReason(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + "\"}";

            var response = await client2.PostAsync(Config.generalUrl + "getAllAchTransactionReason",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));
            return response;
        }

        public static async Task<string> getAllAchTransactionReasonCode(HttpResponseMessage response)
        {
            var res = await response.Content.ReadAsStringAsync();
            return JArray
                .Parse("[" + res + "]")[0]["responseDtos"][0]["code"]
                .ToString();
        }
        public static async Task<string> getAllAchTransactionReasonTitle(HttpResponseMessage response)
        {
            var res = await response.Content.ReadAsStringAsync();
            return JArray.Parse("[" + res + "]")[0]["responseDtos"][0]["description"].ToString();
        }


        public static async Task<HttpResponseMessage> cardHolderInquiry(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""destinationPAN"":""" + Config.destinationPAN + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""sourcePAN"":""" + Config.pan + "\"}";

            var response = await client2.PostAsync(Config.cardUrl + "cardHolderInquiry",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }
        //?
        public static async Task<string> getApprovalCode(HttpResponseMessage response)
        {
            var res = await response.Content.ReadAsStringAsync();
            return JArray.Parse("[" + res + "]")[0]["twoPhaseReferenceCode"].ToString();
        }

        public static async Task<HttpResponseMessage> getCardsByDeposit(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + @"""
                                                  ,""channel"":""" + Config.channel + @"""
                                                  ,""depositNumber"":""" + Config.depositNumber + @"""
                                                  ,""authorizedUserInfo"":"""
                                                  + token + "\"" +
                                                  @",""cardStatus"":""" + "OK" + @"""
                                                  ,""offset"":""" + "1" + @"""
                                                  ,""length"":""" + "1" + "\"}";

            var response = await client2.PostAsync(Config.cardUrl + "getCardsByDeposit",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }

        public static async Task<HttpResponseMessage> shahkar(string token)
        {
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var con = @"{""acceptorCode"":""" + Config.acceptorCode + @"""
                                                  ,""identificationNumber"":""" + Config.identificationNumber + @"""
                                                  ,""identificationType"":""" + "identificationType" + @"""
                                                  ,""mobileNumber"":""" + Config.mobileNumber + @"""
                                                  ,""clientAddress"":""" + Config.clientAddress + "\"}";

            var response = await client2.PostAsync(Config.generalUrl + "shahkar",
                                                  new StringContent(con,
                                                  Encoding.UTF8,
                                                  "application/json"));

            return response;
        }
    }
}
