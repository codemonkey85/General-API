﻿namespace GeneralApi;

public class AppSettings
{
    [JsonPropertyName("baseId")]
    public string? BaseId { get; set; }

    [JsonPropertyName("appKey")]
    public string? AppKey { get; set; }
}
