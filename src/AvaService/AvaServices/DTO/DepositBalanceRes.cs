using System.Text.Json.Serialization;

namespace Sample.AvaServices.DTO;
public class DepositBalanceRes
{
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("referenceNumber")]
    public string ReferenceNumber { get; set; }

    [JsonPropertyName("transactionDate")]
    public string TransactionDate { get; set; }

    [JsonPropertyName("balance")]
    public string Balance { get; set; }
}