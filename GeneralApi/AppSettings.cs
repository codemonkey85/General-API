namespace GeneralApi;

public class AppSettings
{
    public const string SectionName = "AppSettings";

    [JsonPropertyName("baseId")]
    public string? BaseId { get; init; }

    [JsonPropertyName("appKey")]
    public string? AppKey { get; init; }
}
