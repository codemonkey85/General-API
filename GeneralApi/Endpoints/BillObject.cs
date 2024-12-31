using System.Text.Json.Serialization;

namespace GeneralApi.Endpoints;

// ReSharper disable once ClassNeverInstantiated.Global
public class BillObject
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("dueDate")]
    public string DueDate { get; set; } = string.Empty;

    [JsonPropertyName("amountDue")]
    public string AmountDue { get; set; } = string.Empty;
}
