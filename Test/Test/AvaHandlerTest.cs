using System;
using System.Threading.Tasks;
using Sample.AvaServices.DTO;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
using Sample;
using Sample.AvaServices;

namespace Equinox.Core.Test.Test;

public class AvaHandlerTest
{
    public AvaHandlerTest()
    {
        //Arrange

    }

    [Fact]
    public async Task<string> Should_Return_Login_Token()
    {
        //Act
        var response = await AvaLogin.getToken(Program.Encrypt(Config.userName + "|" + Config.password + "|"));
        var loginDto = JsonConvert.DeserializeObject<LoginRes>(response.Content.ReadAsStringAsync().Result);
        Console.WriteLine("Token:  " + loginDto.Token);
        //Assert
        ((int)response.StatusCode).ShouldBe(200);
        loginDto.Token.ShouldNotBeNull();
        loginDto.Token.ShouldBeOfType(typeof(string));
        return loginDto.Token;
    }

    [Fact]
    public async void Should_Return_Balance_Info()
    {
        //Act
        var depositBalanceRes = await AvaDeposit.getDepositBalance(await Should_Return_Login_Token());
        //var depositBalance = JsonConvert.DeserializeObject(depositBalanceRes);
        var depositBalance = JsonConvert.DeserializeObject<DepositBalanceRes>(depositBalanceRes.Content.ReadAsStringAsync().Result);
        //Assert
        ((int)depositBalanceRes.StatusCode).ShouldBe(200);
        depositBalance.Balance.ShouldNotBeNull();
        depositBalance.Balance.ShouldBeOfType(typeof(string));
    }

    [Fact]
    public async void Should_Return_Deposits_Result()
    {
        //Act
        var depositsRes = await AvaDeposit.getDeposits(await Should_Return_Login_Token());
        var deposits = JsonConvert.DeserializeObject<DepositsRes>(depositsRes.Content.ReadAsStringAsync().Result);
        //Assert
        ((int)depositsRes.StatusCode).ShouldBe(200);
        deposits.deposits[0].ShouldNotBeNull();
        deposits.deposits[0].Balance.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async void Should_Return_PolTransfer_Result()
    {
        //Act
        var polTransferRes = await AvaDeposit.PolTransfer(await Should_Return_Login_Token());
        var polTransfer = JsonConvert.DeserializeObject(polTransferRes.Content.ReadAsStringAsync().Result);
        //Assert
        ((int)polTransferRes.StatusCode).ShouldBe(200);
        polTransfer.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_normalTransferOtp()
    {
        //Act
        var normalTransferOtpRes = await AvaDeposit.normalTransferOtp(await Should_Return_Login_Token());
        var normalTransferOtp = JsonConvert.DeserializeObject(normalTransferOtpRes);
        //Assert
        normalTransferOtp.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_achNormalTransferOtp()
    {
        //Act
        var archNormalTransferOtpRes = await AvaDeposit.achNormalTransferOtp(await Should_Return_Login_Token());
        var archNormalTransferOtp = JsonConvert.DeserializeObject(archNormalTransferOtpRes);
        //Assert
        archNormalTransferOtp.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_getCardsByDepositThirdParty()
    {
        //Act
        var getCardsByDepositThirdPartyRes = await AvaDeposit.getCardsByDepositThirdParty("");
        var getCardsByDepositThirdParty = JsonConvert.DeserializeObject(getCardsByDepositThirdPartyRes);
        //Assert
        getCardsByDepositThirdParty.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_getCardBalance()
    {
        //Act
        var getCardBalanceRes = await AvaDeposit.getCardBalance(await Should_Return_Login_Token());
        var getCardBalance = JsonConvert.DeserializeObject(getCardBalanceRes);
        //Assert
        getCardBalance.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_harimOtp()
    {
        //Act
        var harimOtpRes = await AvaDeposit.harimOtp("");
        var harimOtp = JsonConvert.DeserializeObject(harimOtpRes);
        //Assert
        harimOtp.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_getDepositStatement()
    {
        //Act
        var getDepositStatementRes = await AvaDeposit.getDepositStatement(await Should_Return_Login_Token());
        var getDepositStatement = JsonConvert.DeserializeObject(getDepositStatementRes);
        //Assert
        getDepositStatement.ShouldNotBeNull();
    }

    [Fact]
    public async void Should_Return_normalTransferWithThirdParty()
    {
        //Act
        var normalTransferWithThirdPartyRes = await AvaDeposit.normalTransferWithThirdParty(await Should_Return_Login_Token());
        var normalTransferWithThirdParty = JsonConvert.DeserializeObject(normalTransferWithThirdPartyRes);
        //Assert
        normalTransferWithThirdParty.ShouldNotBeNull();
    }
}