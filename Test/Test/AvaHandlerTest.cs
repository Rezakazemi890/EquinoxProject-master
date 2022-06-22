using System;
using System.Threading.Tasks;
using Sample.AvaServices.DTO;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
using Sample;
using Sample.AvaServices;
using Sample.AvaServices.Enum;
using Sample.Utils.cs;

namespace Equinox.Core.Test.Test;

public class AvaHandlerTest
{
    public AvaHandlerTest()
    {
        //Arrange

    }
    private static bool loginLoged;
    [Fact]
    public async Task<string> Should_Return_Login_Token()
    {
        //Act
        DateTime from = DateTime.Now;
        var response = await AvaLogin.getToken(Program.Encrypt(Config.userName + "|" + Config.password + "|"));
        DateTime to = DateTime.Now;
        var loginDto = JsonConvert.DeserializeObject<LoginRes>(await response.Content.ReadAsStringAsync());
        var res = await response.Content.ReadAsStringAsync();
        if (!loginLoged)
        {
            loginLoged = true;
            //Log
            string sep = "----------------------------------------------------------------------------------------------------";
            string log = string.Format("mobileLogin Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}{5}{0}"
            , Environment.NewLine
            , response.RequestMessage.RequestUri,
            (await response.RequestMessage.Content.ReadAsStringAsync())
            , res
            , (int)(response.StatusCode)
            , sep
            , (to - from));

            CreateLogFile.AddToTxtFile(log);
        }
        //Assert
        ((int)response.StatusCode).ShouldBe(200);
        loginDto.Token.ShouldNotBeNull();
        loginDto.Token.ShouldBeOfType(typeof(string));
        loginLoged = true;
        return loginDto.Token;
    }

    [Fact]
    public async void Should_Return_Balance_Info()
    {
        //Act        
        DateTime from = DateTime.Now;
        var depositBalanceRes = await AvaDeposit.getDepositBalance(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        //var depositBalance = JsonConvert.DeserializeObject(depositBalanceRes);
        var depositBalance = JsonConvert.DeserializeObject<DepositBalanceRes>(await depositBalanceRes.Content.ReadAsStringAsync());
        var res = await depositBalanceRes.Content.ReadAsStringAsync();
        var reqprop = await depositBalanceRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("depositBalance Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , depositBalanceRes.RequestMessage.RequestUri,
        (await depositBalanceRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(depositBalanceRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)depositBalanceRes.StatusCode).ShouldBe(200);
        depositBalance.Balance.ShouldNotBeNull();
        depositBalance.Balance.ShouldBeOfType(typeof(string));
    }

    [Fact]
    public async void Should_Return_Deposits_Result()
    {
        //Act
        DateTime from = DateTime.Now;
        var depositsRes = await AvaDeposit.getDeposits(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var deposits = JsonConvert.DeserializeObject<DepositsRes>(await depositsRes.Content.ReadAsStringAsync());
        var res = await depositsRes.Content.ReadAsStringAsync();
        var reqprop = await depositsRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("deposits Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , depositsRes.RequestMessage.RequestUri,
        (await depositsRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(depositsRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)depositsRes.StatusCode).ShouldBe(200);
        deposits.deposits[0].ShouldNotBeNull();
        deposits.deposits[0].Balance.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async void Should_Return_PolTransfer_Result()
    {
        //Act
        DateTime from = DateTime.Now;
        var polTransferRes = await AvaDeposit.PolTransfer(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var polTransfer = JsonConvert.DeserializeObject(await polTransferRes.Content.ReadAsStringAsync());
        var res = await polTransferRes.Content.ReadAsStringAsync();
        var reqprop = await polTransferRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("polTransfer Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , polTransferRes.RequestMessage.RequestUri,
        (await polTransferRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(polTransferRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)polTransferRes.StatusCode).ShouldBe(200);
        polTransfer.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_normalTransferOtp()
    {
        //Act
        DateTime from = DateTime.Now;
        var normalTransferOtpRes = await AvaDeposit.normalTransferOtp(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var normalTransferOtp = JsonConvert.DeserializeObject(await normalTransferOtpRes.Content.ReadAsStringAsync());
        var res = await normalTransferOtpRes.Content.ReadAsStringAsync();
        var reqprop = await normalTransferOtpRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("normalTransferOtp Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , normalTransferOtpRes.RequestMessage.RequestUri,
        (await normalTransferOtpRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(normalTransferOtpRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)normalTransferOtpRes.StatusCode).ShouldBe(200);
        normalTransferOtp.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_achNormalTransferOtp()
    {
        //Act
        DateTime from = DateTime.Now;
        var archNormalTransferOtpRes = await AvaDeposit.achNormalTransferOtp(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var archNormalTransferOtp = JsonConvert.DeserializeObject(await archNormalTransferOtpRes.Content.ReadAsStringAsync());
        var res = await archNormalTransferOtpRes.Content.ReadAsStringAsync();
        var reqprop = await archNormalTransferOtpRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("archNormalTransferOtp Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , archNormalTransferOtpRes.RequestMessage.RequestUri,
        (await archNormalTransferOtpRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(archNormalTransferOtpRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);
        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)archNormalTransferOtpRes.StatusCode).ShouldBe(200);
        archNormalTransferOtp.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_getCardsByDepositThirdParty()
    {
        //Act
        DateTime from = DateTime.Now;
        var getCardsByDepositThirdPartyRes = await AvaDeposit.getCardsByDepositThirdParty("");
        DateTime to = DateTime.Now;
        var getCardsByDepositThirdParty = JsonConvert.DeserializeObject(await getCardsByDepositThirdPartyRes.Content.ReadAsStringAsync());
        var res = await getCardsByDepositThirdPartyRes.Content.ReadAsStringAsync();
        var reqprop = await getCardsByDepositThirdPartyRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("getCardsByDepositThirdParty Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , getCardsByDepositThirdPartyRes.RequestMessage.RequestUri,
        (await getCardsByDepositThirdPartyRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(getCardsByDepositThirdPartyRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)getCardsByDepositThirdPartyRes.StatusCode).ShouldBe(200);
        getCardsByDepositThirdParty.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_getCardBalance()
    {
        //Act
        DateTime from = DateTime.Now;
        var getCardBalanceRes = await AvaDeposit.getCardBalance(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var getCardBalance = JsonConvert.DeserializeObject(await getCardBalanceRes.Content.ReadAsStringAsync());
        var res = await getCardBalanceRes.Content.ReadAsStringAsync();
        var reqprop = await getCardBalanceRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("getCardBalance Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , getCardBalanceRes.RequestMessage.RequestUri,
        (await getCardBalanceRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(getCardBalanceRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)getCardBalanceRes.StatusCode).ShouldBe(200);
        getCardBalance.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_harimOtp()
    {
        //Act
        DateTime from = DateTime.Now;
        var harimOtpRes = await AvaDeposit.harimOtp("");
        DateTime to = DateTime.Now;
        var harimOtp = JsonConvert.DeserializeObject(await harimOtpRes.Content.ReadAsStringAsync());
        var res = await harimOtpRes.Content.ReadAsStringAsync();
        var reqprop = await harimOtpRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("harimOtp Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , harimOtpRes.RequestMessage.RequestUri,
        (await harimOtpRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(harimOtpRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)harimOtpRes.StatusCode).ShouldBe(200);
        harimOtp.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_getDepositStatement()
    {
        //Act
        DateTime from = DateTime.Now;
        var getDepositStatementRes = await AvaDeposit.getDepositStatement(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var getDepositStatement = JsonConvert.DeserializeObject(await getDepositStatementRes.Content.ReadAsStringAsync());
        var res = await getDepositStatementRes.Content.ReadAsStringAsync();
        var reqprop = await getDepositStatementRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("getDepositStatement Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , getDepositStatementRes.RequestMessage.RequestUri,
        (await getDepositStatementRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(getDepositStatementRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)getDepositStatementRes.StatusCode).ShouldBe(200);
        getDepositStatement.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_normalTransferWithThirdParty()
    {
        //Act
        DateTime from = DateTime.Now;
        var normalTransferWithThirdPartyRes = await AvaDeposit.normalTransferWithThirdParty(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var normalTransferWithThirdParty = JsonConvert.DeserializeObject(await normalTransferWithThirdPartyRes.Content.ReadAsStringAsync());
        var res = await normalTransferWithThirdPartyRes.Content.ReadAsStringAsync();
        var reqprop = await normalTransferWithThirdPartyRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("normalTransferWithThirdParty Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , normalTransferWithThirdPartyRes.RequestMessage.RequestUri,
        (await normalTransferWithThirdPartyRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(normalTransferWithThirdPartyRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)normalTransferWithThirdPartyRes.StatusCode).ShouldBe(200);
        normalTransferWithThirdParty.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_normalTransfer()
    {
        //Act
        DateTime from = DateTime.Now;
        var normalTransferRes = await AvaDeposit.normalTransfer(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var normalTransfer = JsonConvert.DeserializeObject(await normalTransferRes.Content.ReadAsStringAsync());
        var res = await normalTransferRes.Content.ReadAsStringAsync();
        var reqprop = await normalTransferRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("normalTransfer Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , normalTransferRes.RequestMessage.RequestUri,
        (await normalTransferRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(normalTransferRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)normalTransferRes.StatusCode).ShouldBe(200);
        normalTransfer.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_achNormalTransfer()
    {
        //Act
        DateTime from = DateTime.Now;
        var achNormalTransferRes = await AvaDeposit.achNormalTransfer(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var achNormalTransfer = JsonConvert.DeserializeObject(await achNormalTransferRes.Content.ReadAsStringAsync());
        var res = await achNormalTransferRes.Content.ReadAsStringAsync();
        var reqprop = await achNormalTransferRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("achNormalTransfer Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , achNormalTransferRes.RequestMessage.RequestUri,
        (await achNormalTransferRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(achNormalTransferRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)achNormalTransferRes.StatusCode).ShouldBe(200);
        achNormalTransfer.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_cardToIban()
    {
        //Act
        DateTime from = DateTime.Now;
        var cardToIbanRes = await AvaDeposit.cardToIban(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var cardToIban = JsonConvert.DeserializeObject(await cardToIbanRes.Content.ReadAsStringAsync());
        var res = await cardToIbanRes.Content.ReadAsStringAsync();
        var reqprop = await cardToIbanRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("cardToIban Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , cardToIbanRes.RequestMessage.RequestUri,
        (await cardToIbanRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(cardToIbanRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)cardToIbanRes.StatusCode).ShouldBe(200);
        cardToIban.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_getDepositsByCard()
    {
        //Act
        DateTime from = DateTime.Now;
        var getDepositsByCardRes = await AvaDeposit.getDepositsByCard(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var getDepositsByCard = JsonConvert.DeserializeObject(await getDepositsByCardRes.Content.ReadAsStringAsync());
        var res = await getDepositsByCardRes.Content.ReadAsStringAsync();
        var reqprop = await getDepositsByCardRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (res.Contains(prop))
                mustProperties += "," + prop;
            if (reqprop.Contains(prop))
                mustProperties += "," + prop;
        }
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("getDepositsByCard Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , getDepositsByCardRes.RequestMessage.RequestUri,
        (await getDepositsByCardRes.RequestMessage.Content.ReadAsStringAsync())
        , res
        , (int)(getDepositsByCardRes.StatusCode)
        , sep
        , (to - from)
        , mustProperties);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)getDepositsByCardRes.StatusCode).ShouldBe(200);
        getDepositsByCard.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_depositToIban()
    {
        //Act
        DateTime from = DateTime.Now;
        var depositToIbanRes = await AvaDeposit.depositToIban(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var depositToIban = JsonConvert.DeserializeObject(await depositToIbanRes.Content.ReadAsStringAsync());
        var res = await depositToIbanRes.Content.ReadAsStringAsync();
        var reqprop = await depositToIbanRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, depositToIbanRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)depositToIbanRes.StatusCode).ShouldBe(200);
        depositToIban.ShouldNotBeNull();
    }
}