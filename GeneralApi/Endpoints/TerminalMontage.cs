namespace GeneralApi.Endpoints;

public class TerminalMontage : IEndpoint
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
            "pokemonbattleroyale" => new VideoInfo("PL_nUhPfMOdad6jsHc7Sl_mfCVJFJ7M5TC", true),
            "somethingabout" => new VideoInfo("PL_nUhPfMOdacbmxUn0m8bZf0Gu6SIsTgg", true),
            "allcartoons" => new VideoInfo("PL3A7CB3AB20462EED", true),
            "zeldaoot" => new VideoInfo("PL_nUhPfMOdafrG_2mkNNNwZXYqGXu1Oci", true),

            "sonic1" => new VideoInfo("nTDU9fSMNyI"),
            "sonic2" => new VideoInfo("4mmV3TpYCwc"),
            "yoshisisland" => new VideoInfo("Dk8Qy7L5ZaI"),
            "smashbros" => new VideoInfo("c3ls2y_2UHM"),
            "kirbysuperstar" => new VideoInfo("O8SL3VEQEqQ"),
            "mario64" => new VideoInfo("ff9ldkz1h1M"),
            "donkeykongcountry" => new VideoInfo("kDEBu5orzQU"),
            "starfox64" => new VideoInfo("5KIW8kZnFAg"),
            "zeldabotw" => new VideoInfo("1or3YILu28M"),
            "supermarioworld" => new VideoInfo("_bE3PCRHq-A"),
            "kirbysadventure" => new VideoInfo("fC3pEQB3_Tc"),
            "dittosdaycare" => new VideoInfo("JcuVqiwsiGY"),
            "supermetroid" => new VideoInfo("YcppnDs2Rz8"),
            "yoshisstory" => new VideoInfo("uyKP3U1824s"),
            "kirbyamazingmirror" => new VideoInfo("8JzyquUk1NY"),
            "supermarioallstars" => new VideoInfo("EZQLVKN2SU4"),
            "smashbrossubspaceemissary" => new VideoInfo("5juBmmXoW7I"),
            "kirby64" => new VideoInfo("EY14vhBJMKg"),
            "kirbysdreambuffet" => new VideoInfo("ckwtQLlXZYQ"),
            "pokemonlegendsarceus" => new VideoInfo("Jr7pjruyR9s"),
            _ => new VideoInfo(string.Empty)
        };

        return vidCode is { Length: > 0 }
            ? playlist
                ? $"{VidPlaylistPrefix}{vidCode}"
                : $"{VidCodePrefix}{vidCode}"
            : TerminalMontageYouTube;
    }

    private record VideoInfo(string VidCode, bool Playlist = false);
}
