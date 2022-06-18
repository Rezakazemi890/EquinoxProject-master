using System.Text.Json.Serialization;

namespace Sample.AvaServices.DTO;
public class LoginRes
{
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("referenceNumber")]
    public string ReferenceNumber { get; set; }

    [JsonPropertyName("transactionDate")]
    public string TransactionDate { get; set; }

    [JsonPropertyName("token")]
    public string Token { get; set; }
}