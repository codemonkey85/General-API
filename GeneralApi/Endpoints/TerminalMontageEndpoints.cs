namespace GeneralApi.Endpoints;

public class TerminalMontageEndpoints : IEndpoint
{
    private const string YouTubeUrl = "https://www.youtube.com";
    private const string TerminalMontageYouTube = $"{YouTubeUrl}/@TerminalMontage";
    private const string VidCodePrefix = $"{YouTubeUrl}/watch?v=";
    private const string VidPlaylistPrefix = $"{YouTubeUrl}/playlist?list=";

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder apiGroup)
    {
        var avmGroup = apiGroup.MapGroup("/tm");

        avmGroup.MapGet("/", GetYouTubeUrl);
        // ReSharper disable once ConvertClosureToMethodGroup
        avmGroup.MapGet("/{subject}", (string subject) => GetYouTubeUrl(subject));

        return apiGroup;
    }

    private static string GetYouTubeUrl([FromQuery] string subject = "")
    {
        var (vidCode, playlist) = subject.ToLower() switch
        {
            "pokemonbattleroyale" => new("PL_nUhPfMOdad6jsHc7Sl_mfCVJFJ7M5TC", true),
            "somethingabout" => new("PL_nUhPfMOdacbmxUn0m8bZf0Gu6SIsTgg", true),
            "allcartoons" => new("PL3A7CB3AB20462EED", true),
            "zeldaoot" => new("PL_nUhPfMOdafrG_2mkNNNwZXYqGXu1Oci", true),

            "sonic1" => new("nTDU9fSMNyI"),
            "sonic2" => new("4mmV3TpYCwc"),
            "yoshisisland" => new("Dk8Qy7L5ZaI"),
            "smashbros" => new("c3ls2y_2UHM"),
            "kirbysuperstar" => new("O8SL3VEQEqQ"),
            "mario64" => new("ff9ldkz1h1M"),
            "donkeykongcountry" => new("kDEBu5orzQU"),
            "starfox64" => new("5KIW8kZnFAg"),
            "zeldabotw" => new("1or3YILu28M"),
            "supermarioworld" => new("_bE3PCRHq-A"),
            "kirbysadventure" => new("fC3pEQB3_Tc"),
            "dittosdaycare" => new("JcuVqiwsiGY"),
            "supermetroid" => new("YcppnDs2Rz8"),
            "yoshisstory" => new("uyKP3U1824s"),
            "kirbyamazingmirror" => new("8JzyquUk1NY"),
            "supermarioallstars" => new("EZQLVKN2SU4"),
            "smashbrossubspaceemissary" => new("5juBmmXoW7I"),
            "kirby64" => new("EY14vhBJMKg"),
            "kirbysdreambuffet" => new("ckwtQLlXZYQ"),
            "pokemonlegendsarceus" => new("Jr7pjruyR9s"),
            _ => new VideoInfo(string.Empty),
        };

        return vidCode is { Length: > 0 }
            ? playlist
                ? $"{VidPlaylistPrefix}{vidCode}"
                : $"{VidCodePrefix}{vidCode}"
            : TerminalMontageYouTube;
    }

    private record VideoInfo(string VidCode, bool Playlist = false);
}
