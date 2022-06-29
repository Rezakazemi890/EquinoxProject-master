using System;
using System.Threading.Tasks;
using Sample.AvaServices.DTO;
using Newtonsoft.Json;
using Shouldly;
using Xunit;
using Sample;
using Sample.AvaServices;
using Sample.AvaServices.Enum;
using Equinox.Core.Test.TestUtils;
using Sample.Utils.cs;

namespace Equinox.Core.Test.Test;

[TestCaseOrderer("Equinox.Core.Test.TestUtils.PriorityOrderer", "Equinox.Core.Test")]
public class AvaHandlerTest
{
    private static bool loginLoged;
    private static int passTest;
    private static int allTest;

    [Fact, TestPriority(1)]
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
            allTest++;
            loginLoged = true;
            //Log
            string log = await Utils.GetStatusLog(res, response, string.Empty, from, to);

            CreateLogFile.AddToTxtFile(log);
        }
        //Assert
        ((int)response.StatusCode).ShouldBe(200);
        loginDto.Token.ShouldNotBeNull();
        loginDto.Token.ShouldBeOfType(typeof(string));
        if (!loginLoged)
            passTest++;
        loginLoged = true;

        return loginDto.Token;
    }

    [Fact, TestPriority(2)]
    public async void Should_Return_Balance_Info()
    {
        allTest++;
        //Act        
        DateTime from = DateTime.Now;
        var depositBalanceRes = await AvaDeposit.getDepositBalance(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        //var depositBalance = JsonConvert.DeserializeObject(depositBalanceRes);
        var depositBalance = JsonConvert.DeserializeObject<DepositBalanceRes>(await depositBalanceRes.Content.ReadAsStringAsync());
        var res = await depositBalanceRes.Content.ReadAsStringAsync();
        var reqprop = await depositBalanceRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, depositBalanceRes, mustProperties, from, to);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)depositBalanceRes.StatusCode).ShouldBe(200);
        depositBalance.Balance.ShouldNotBeNull();
        depositBalance.Balance.ShouldBeOfType(typeof(string));
        passTest++;
    }

    [Fact, TestPriority(3)]
    public async void Should_Return_Deposits_Result()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var depositsRes = await AvaDeposit.getDeposits(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var deposits = JsonConvert.DeserializeObject<DepositsRes>(await depositsRes.Content.ReadAsStringAsync());
        var res = await depositsRes.Content.ReadAsStringAsync();
        var reqprop = await depositsRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, depositsRes, mustProperties, from, to);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)depositsRes.StatusCode).ShouldBe(200);
        deposits.deposits[0].ShouldNotBeNull();
        deposits.deposits[0].Balance.ShouldBeGreaterThan(0);
        passTest++;
    }

    [Fact, TestPriority(4)]
    public async void Should_Return_PolTransfer_Result()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var polTransferRes = await AvaDeposit.PolTransfer(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var polTransfer = JsonConvert.DeserializeObject(await polTransferRes.Content.ReadAsStringAsync());
        var res = await polTransferRes.Content.ReadAsStringAsync();
        var reqprop = await polTransferRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, polTransferRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)polTransferRes.StatusCode).ShouldBe(200);
        polTransfer.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(5)]
    public async void Should_Return_normalTransferOtp()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var normalTransferOtpRes = await AvaDeposit.normalTransferOtp(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var normalTransferOtp = JsonConvert.DeserializeObject(await normalTransferOtpRes.Content.ReadAsStringAsync());
        var res = await normalTransferOtpRes.Content.ReadAsStringAsync();
        var reqprop = await normalTransferOtpRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, normalTransferOtpRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)normalTransferOtpRes.StatusCode).ShouldBe(200);
        normalTransferOtp.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(6)]
    public async void Should_Return_achNormalTransferOtp()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var archNormalTransferOtpRes = await AvaDeposit.achNormalTransferOtp(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var archNormalTransferOtp = JsonConvert.DeserializeObject(await archNormalTransferOtpRes.Content.ReadAsStringAsync());
        var res = await archNormalTransferOtpRes.Content.ReadAsStringAsync();
        var reqprop = await archNormalTransferOtpRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, archNormalTransferOtpRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)archNormalTransferOtpRes.StatusCode).ShouldBe(200);
        archNormalTransferOtp.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(6)]
    public async void Should_Return_getCardsByDepositThirdParty()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var getCardsByDepositThirdPartyRes = await AvaDeposit.getCardsByDepositThirdParty("");
        DateTime to = DateTime.Now;
        var getCardsByDepositThirdParty = JsonConvert.DeserializeObject(await getCardsByDepositThirdPartyRes.Content.ReadAsStringAsync());
        var res = await getCardsByDepositThirdPartyRes.Content.ReadAsStringAsync();
        var reqprop = await getCardsByDepositThirdPartyRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, getCardsByDepositThirdPartyRes, mustProperties, from, to);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)getCardsByDepositThirdPartyRes.StatusCode).ShouldBe(200);
        getCardsByDepositThirdParty.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(7)]
    public async void Should_Return_getCardBalance()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var getCardBalanceRes = await AvaDeposit.getCardBalance(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var getCardBalance = JsonConvert.DeserializeObject(await getCardBalanceRes.Content.ReadAsStringAsync());
        var res = await getCardBalanceRes.Content.ReadAsStringAsync();
        var reqprop = await getCardBalanceRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, getCardBalanceRes, mustProperties, from, to);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)getCardBalanceRes.StatusCode).ShouldBe(200);
        getCardBalance.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(8)]
    public async void Should_Return_harimOtp()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var harimOtpRes = await AvaDeposit.harimOtp("");
        DateTime to = DateTime.Now;
        var harimOtp = JsonConvert.DeserializeObject(await harimOtpRes.Content.ReadAsStringAsync());
        var res = await harimOtpRes.Content.ReadAsStringAsync();
        var reqprop = await harimOtpRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, harimOtpRes, mustProperties, from, to);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)harimOtpRes.StatusCode).ShouldBe(200);
        harimOtp.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(9)]
    public async void Should_Return_getDepositStatement()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var getDepositStatementRes = await AvaDeposit.getDepositStatement(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var getDepositStatement = JsonConvert.DeserializeObject(await getDepositStatementRes.Content.ReadAsStringAsync());
        var res = await getDepositStatementRes.Content.ReadAsStringAsync();
        var reqprop = await getDepositStatementRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, getDepositStatementRes, mustProperties, from, to);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)getDepositStatementRes.StatusCode).ShouldBe(200);
        getDepositStatement.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(10)]
    public async void Should_Return_normalTransferWithThirdParty()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var normalTransferWithThirdPartyRes = await AvaDeposit.normalTransferWithThirdParty(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var normalTransferWithThirdParty = JsonConvert.DeserializeObject(await normalTransferWithThirdPartyRes.Content.ReadAsStringAsync());
        var res = await normalTransferWithThirdPartyRes.Content.ReadAsStringAsync();
        var reqprop = await normalTransferWithThirdPartyRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, normalTransferWithThirdPartyRes, mustProperties, from, to);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)normalTransferWithThirdPartyRes.StatusCode).ShouldBe(200);
        normalTransferWithThirdParty.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(11)]
    public async void Should_Return_normalTransfer()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var normalTransferRes = await AvaDeposit.normalTransfer(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var normalTransfer = JsonConvert.DeserializeObject(await normalTransferRes.Content.ReadAsStringAsync());
        var res = await normalTransferRes.Content.ReadAsStringAsync();
        var reqprop = await normalTransferRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, normalTransferRes, mustProperties, from, to);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)normalTransferRes.StatusCode).ShouldBe(200);
        normalTransfer.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(12)]
    public async void Should_Return_achNormalTransfer()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var achNormalTransferRes = await AvaDeposit.achNormalTransfer(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var achNormalTransfer = JsonConvert.DeserializeObject(await achNormalTransferRes.Content.ReadAsStringAsync());
        var res = await achNormalTransferRes.Content.ReadAsStringAsync();
        var reqprop = await achNormalTransferRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, achNormalTransferRes, mustProperties, from, to);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)achNormalTransferRes.StatusCode).ShouldBe(200);
        achNormalTransfer.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(13)]
    public async void Should_Return_cardToIban()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var cardToIbanRes = await AvaDeposit.cardToIban(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var cardToIban = JsonConvert.DeserializeObject(await cardToIbanRes.Content.ReadAsStringAsync());
        var res = await cardToIbanRes.Content.ReadAsStringAsync();
        var reqprop = await cardToIbanRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, cardToIbanRes, mustProperties, from, to);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)cardToIbanRes.StatusCode).ShouldBe(200);
        cardToIban.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(14)]
    public async void Should_Return_getDepositsByCard()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var getDepositsByCardRes = await AvaDeposit.getDepositsByCard(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var getDepositsByCard = JsonConvert.DeserializeObject(await getDepositsByCardRes.Content.ReadAsStringAsync());
        var res = await getDepositsByCardRes.Content.ReadAsStringAsync();
        var reqprop = await getDepositsByCardRes.RequestMessage.Content.ReadAsStringAsync();

        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, getDepositsByCardRes, mustProperties, from, to);

        CreateLogFile.AddToTxtFile(log);

        //Assert
        ((int)getDepositsByCardRes.StatusCode).ShouldBe(200);
        getDepositsByCard.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(15)]
    public async void Should_Return_depositToIban()
    {
        allTest++;
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
        passTest++;
    }

    [Fact, TestPriority(16)]
    public async void Should_Return_changeTransactionSecondPassword()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var changeTransactionSecondPasswordRes = await AvaDeposit.changeTransactionSecondPassword(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var changeTransactionSecondPassword = JsonConvert.DeserializeObject(await changeTransactionSecondPasswordRes.Content.ReadAsStringAsync());
        var res = await changeTransactionSecondPasswordRes.Content.ReadAsStringAsync();
        var reqprop = await changeTransactionSecondPasswordRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, changeTransactionSecondPasswordRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)changeTransactionSecondPasswordRes.StatusCode).ShouldBe(200);
        changeTransactionSecondPassword.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(17)]
    public async void Should_Return_changeCardStaticPin()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var changeCardStaticPinRes = await AvaDeposit.changeCardStaticPin(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var changeCardStaticPin = JsonConvert.DeserializeObject(await changeCardStaticPinRes.Content.ReadAsStringAsync());
        var res = await changeCardStaticPinRes.Content.ReadAsStringAsync();
        var reqprop = await changeCardStaticPinRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, changeCardStaticPinRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)changeCardStaticPinRes.StatusCode).ShouldBe(200);
        changeCardStaticPin.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(18)]
    public async void Should_Return_changeCardSecondOtpStatus()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var changeCardSecondOtpStatusRes = await AvaDeposit.changeCardSecondOtpStatus(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var changeCardSecondOtpStatus = JsonConvert.DeserializeObject(await changeCardSecondOtpStatusRes.Content.ReadAsStringAsync());
        var res = await changeCardSecondOtpStatusRes.Content.ReadAsStringAsync();
        var reqprop = await changeCardSecondOtpStatusRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, changeCardSecondOtpStatusRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)changeCardSecondOtpStatusRes.StatusCode).ShouldBe(200);
        changeCardSecondOtpStatus.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(19)]
    public async void Should_Return_generateCardPin()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var generateCardPinRes = await AvaDeposit.generateCardPin(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var generateCardPin = JsonConvert.DeserializeObject(await generateCardPinRes.Content.ReadAsStringAsync());
        var res = await generateCardPinRes.Content.ReadAsStringAsync();
        var reqprop = await generateCardPinRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, generateCardPinRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)generateCardPinRes.StatusCode).ShouldBe(200);
        generateCardPin.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(20)]
    public async void Should_Return_hotCard()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var hotCardRes = await AvaDeposit.hotCard(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var hotCard = JsonConvert.DeserializeObject(await hotCardRes.Content.ReadAsStringAsync());
        var res = await hotCardRes.Content.ReadAsStringAsync();
        var reqprop = await hotCardRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, hotCardRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)hotCardRes.StatusCode).ShouldBe(200);
        hotCard.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(21)]
    public async void Should_Return_activateCard()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var activateCardRes = await AvaDeposit.activateCard(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var activateCard = JsonConvert.DeserializeObject(await activateCardRes.Content.ReadAsStringAsync());
        var res = await activateCardRes.Content.ReadAsStringAsync();
        var reqprop = await activateCardRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, activateCardRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)activateCardRes.StatusCode).ShouldBe(200);
        activateCard.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(22)]
    public async void Should_Return_cardTransfer()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var cardTransferRes = await AvaDeposit.cardTransfer(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var cardTransfer = JsonConvert.DeserializeObject(await cardTransferRes.Content.ReadAsStringAsync());
        var res = await cardTransferRes.Content.ReadAsStringAsync();
        var reqprop = await cardTransferRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, cardTransferRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)cardTransferRes.StatusCode).ShouldBe(200);
        cardTransfer.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(23)]
    public async void Should_Return_cardTransferOtp()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var cardTransferOtpRes = await AvaDeposit.cardTransferOtp(string.Empty);
        DateTime to = DateTime.Now;
        var cardTransferOtp = JsonConvert.DeserializeObject(await cardTransferOtpRes.Content.ReadAsStringAsync());
        var res = await cardTransferOtpRes.Content.ReadAsStringAsync();
        var reqprop = await cardTransferOtpRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, cardTransferOtpRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)cardTransferOtpRes.StatusCode).ShouldBe(200);
        cardTransferOtp.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(24)]
    public async void Should_Return_cardHolderInquiry()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var cardHolderInquiryRes = await AvaDeposit.cardHolderInquiry(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var cardHolderInquiry = JsonConvert.DeserializeObject(await cardHolderInquiryRes.Content.ReadAsStringAsync());
        var res = await cardHolderInquiryRes.Content.ReadAsStringAsync();
        var reqprop = await cardHolderInquiryRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, cardHolderInquiryRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)cardHolderInquiryRes.StatusCode).ShouldBe(200);
        cardHolderInquiry.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(25)]
    public async void Should_Return_payLoan()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var payLoanRes = await AvaDeposit.payLoan(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var payLoan = JsonConvert.DeserializeObject(await payLoanRes.Content.ReadAsStringAsync());
        var res = await payLoanRes.Content.ReadAsStringAsync();
        var reqprop = await payLoanRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, payLoanRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)payLoanRes.StatusCode).ShouldBe(200);
        payLoan.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(26)]
    public async void Should_Return_getLoans()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var getLoansRes = await AvaDeposit.getLoans(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var getLoans = JsonConvert.DeserializeObject(await getLoansRes.Content.ReadAsStringAsync());
        var res = await getLoansRes.Content.ReadAsStringAsync();
        var reqprop = await getLoansRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, getLoansRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)getLoansRes.StatusCode).ShouldBe(200);
        getLoans.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(27)]
    public async void Should_Return_getLoanDetail()
    {
        allTest++;
        //Act
        DateTime from = DateTime.Now;
        var getLoanDetailRes = await AvaDeposit.getLoanDetail(await Should_Return_Login_Token());
        DateTime to = DateTime.Now;
        var getLoanDetail = JsonConvert.DeserializeObject(await getLoanDetailRes.Content.ReadAsStringAsync());
        var res = await getLoanDetailRes.Content.ReadAsStringAsync();
        var reqprop = await getLoanDetailRes.RequestMessage.Content.ReadAsStringAsync();
        string mustProperties = Utils.GetMustSaveProperties(res, reqprop);
        string log = await Utils.GetStatusLog(res, getLoanDetailRes, mustProperties, from, to);
        CreateLogFile.AddToTxtFile(log);
        //Assert
        ((int)getLoanDetailRes.StatusCode).ShouldBe(200);
        getLoanDetail.ShouldNotBeNull();
        passTest++;
    }

    [Fact, TestPriority(28)]
    public async void Should_Return_SummeryOfTest()
    {
        //Act
        string log = @"----------------------------------------------------------------------------------------------------";
        log = log
            + Environment.NewLine
            + "All test : "
            + allTest
            + " - Pass : "
            + passTest
            + " - Fail : "
            + (allTest - passTest);
        CreateLogFile.AddToTxtFile(log);
    }
}