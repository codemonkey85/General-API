namespace GeneralApi;

public class AppSettings
{
    public const string SectionName = "AppSettings";

    [JsonPropertyName("baseId")]
    public string? BaseId { get; set; }

    [JsonPropertyName("appKey")]
    public string? AppKey { get; set; }
}
