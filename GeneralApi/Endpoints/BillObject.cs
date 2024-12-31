using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace GeneralApi.Endpoints;

// ReSharper disable once ClassNeverInstantiated.Global
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global"),
 SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class BillObject
{
    [JsonPropertyName("title")] public string Title { get; set; } = string.Empty;

    [JsonPropertyName("dueDate")] public string DueDate { get; set; } = string.Empty;

    [JsonPropertyName("amountDue")] public string AmountDue { get; set; } = string.Empty;

    [JsonIgnore] public DateTime? DueDateDisplay => DateTime.TryParse(DueDate.Trim(), out var date) ? date : null;

    [JsonIgnore]
    public decimal? AmountDueDisplay =>
        decimal.TryParse(AmountDue.Replace("$", string.Empty).Trim(), out var amount) ? amount : null;

    [JsonIgnore] public bool IsOverdue => DueDateDisplay < DateTime.Today;
}
