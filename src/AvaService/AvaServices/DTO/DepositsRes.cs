using System.Text.Json.Serialization;

namespace Sample.AvaServices.DTO;
public class DepositsRes
{
    [JsonPropertyName("deposits")]
    public DepositBean[] deposits { get; set; }

    [JsonPropertyName("referenceNumber")]
    public string ReferenceNumber { get; set; }

    [JsonPropertyName("transactionDate")]
    public string TransactionDate { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }
}

public class DepositBean
{
    [JsonPropertyName("availableBalance")]
    public Int64 AvailableBalance { get; set; }

    [JsonPropertyName("balance")]
    public Int64 Balance { get; set; }

    [JsonPropertyName("referenceNumber")]
    public string ReferenceNumber { get; set; }

    [JsonPropertyName("branchCode")]
    public string BranchCode { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("depositNumber")]
    public string DepositNumber { get; set; }

    [JsonPropertyName("depositStatus")]
    public string DepositStatus { get; set; }

    [JsonPropertyName("depositTitle")]
    public string DepositTitle { get; set; }

    [JsonPropertyName("ibanNumber")]
    public string IbanNumber { get; set; }

    [JsonPropertyName("owner")]
    public string Owner { get; set; }

    [JsonPropertyName("signature")]
    public string Signature { get; set; }
}