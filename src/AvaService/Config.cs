namespace Sample
{
    public static class Config
    {
        public static string userName = "guestUser1";
        public static string password = "wsx@qwe120";
        public static string acceptorCode = "6901000000";
        public static string clientAddress = "5.160.235.50";
        public static string channel = "MOBILE";
        public static string depositNumber = "131-800-1000000-1";
        public static string sourceDescription = "test";
        public static string amount = "10000";
        public static string destinationIBAN = "IR123456789012345678902222";
        //?
        public static string destinationIbanOwnerName = "test";
        //?
        public static string reasonCode = "001";
        //?
        public static string reasonTitle = "test";
        public static string sourceDeposit = "131-800-1000000-1";
        //?
        public static string destinationDeposit = "100-100-1000000-1";
        public static string cvv2 = "000";
        public static string expDate = "0409";
        public static string pan = "5057851000000000";
        //?
        public static string destinationPAN = "5057851000000000";
        //?
        public static string pin = "123456";
        public static string fromDate = "2022-04-30T00:00:00";
        public static string toDate = "2022-05-15T00:00:00";
        public static string ticket = "123456";
        //?
        public static string approvalCode = "123456";
        //?
        public static string trackingNumber = "1234567890";
        public static string loanNumber = "101.101.1000000.101";
        //?
        public static string branchCode = "123456";
        //?
        public static string cbLoanNumber = "123456";
        //?
        public static string loanTitle = "Test";
        public static string secondPassword = "654321";
        public static string identificationNumber = "0000000000";
        public static string mobileNumber = "09121212020";
        public static string depositUrl = "Https://sbxapi.izbank.ir/private/deposit/v1/";
        public static string loginUrl = "https://sbxapi.izbank.ir/login/v1/";
        public static string cardUrl = "https://sbxapi.izbank.ir/private/card/v1/";
        public static string generalUrl = "Https://sbxapi.izbank.ir/private/general-services/v1/";
        public static string loanUrl = "https://sbxapi.izbank.ir/private/loan/v1/";
        public static string logPath =
            string.Format(@"D:\AvaResult-{0}-{1}.txt",
            DateTime.Now.ToShortDateString().
            Replace("/", "-").
            Replace(":", "-"),
            DateTime.Now.ToShortTimeString().
            Replace("/", "-").
            Replace(":", "-")
            );
    }
}
