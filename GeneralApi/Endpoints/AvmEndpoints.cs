﻿namespace GeneralApi.Endpoints;

public class AvmEndpoints : IEndpoint
{
    private const string youTubeUrl = "https://www.youtube.com";
    private const string alanBeckerYouTube = $"{youTubeUrl}/@alanbecker";
    private const string vidCodePrefix = $"{youTubeUrl}/watch?v=";
    private const string playlistCodePrefix = $"{youTubeUrl}/playlist?list="

    public void MapEndpoints(IEndpointRouteBuilder apiGroup)
    {
        var avmGroup = apiGroup.MapGroup("/avm");

        avmGroup.MapGet("/", GetYouTubeUrl);
        avmGroup.MapGet("/{episodeNumber}", (int episodeNumber) => GetYouTubeUrl(episodeNumber));
        avmGroup.MapGet("/playlist", GetPlaylistUrl);
    }
    
    private static string GetPlaylistUrl() =>
        $"{playlistCodePrefix}PL7z8SQeih5AdUZvp2JUdYW7WKfF9xa7Rh";

    private static string GetYouTubeUrl([FromQuery] int episodeNumber = 0)
    {
        var vidCode = episodeNumber switch
        {
            01 => "sVhWjSKywAQ",
            02 => "J_PRXuovweE",
            03 => "cPWPAjYb5eI",
            04 => "KXgaSaME-Ms",
            05 => "c9DrryMw248",
            06 => "y5zW1c30MGQ",
            07 => "z2sRTaa9lZU",
            08 => "E7x2pH6yiL4",
            09 => "_fPhQLM4Iac",
            10 => "wkedWWxRABM",
            11 => "WTPt07NO61A",
            12 => "O3JPzQYJTZk",
            13 => "zRD4-cl9_OU",
            14 => "pCBP4M08ndE",
            15 => "X7Mpp35EAqI",
            16 => "c0egcErkap0",
            17 => "WDQ60JGcxkw",
            18 => "ZXUCbK3Au4k",
            19 => "en5KyaRyRbw",
            20 => "H_4e85Q2EjE",
            21 => "-Y2Mi2iAVMY",
            22 => "q4dANhP7BVg",
            23 => "L_Z7lQTtJK8",
            24 => "yPCSUE0-4qI",
            25 => "8kiPjEeTaW4",
            26 => "YZ4W964hocA",
            27 => "SNuJrSP3JDA",
            28 => "o5RhbG3tOT8",
            29 => "4P6l35M61GY",
            30 => "Sp2nxlrQ89w",
            31 => "mOo9iTx0aMQ",
            32 => "7UPEJjZ0UAE",
            33 => "vlVUVsx6BYY",
            _ => string.Empty,
        };

        return vidCode is { Length: > 0 }
            ? $"{vidCodePrefix}{vidCode}"
            : alanBeckerYouTube;
    }
}
