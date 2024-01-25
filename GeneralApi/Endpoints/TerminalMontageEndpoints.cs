namespace GeneralApi.Endpoints;

public class TerminalMontageEndpoints : IEndpoint
{
    private const string youTubeUrl = "https://www.youtube.com";
    private const string terminalMontageYouTube = $"{youTubeUrl}/@TerminalMontage";
    private const string vidCodePrefix = $"{youTubeUrl}/watch?v=";
    private const string vidPlaylistPrefix = $"{youTubeUrl}/playlist?list=";

    public void MapEndpoints(IEndpointRouteBuilder apiGroup)
    {
        var avmGroup = apiGroup.MapGroup("/tm");

        avmGroup.MapGet("/", GetYouTubeUrl);
        avmGroup.MapGet("/{subject}", (string subject) => GetYouTubeUrl(subject));
    }

    private static string GetYouTubeUrl([FromQuery] string subject = "")
    {
        var (vidCode, playlist) = subject.ToLower() switch
        {
            "pokemonbattleroyale" => ("PL_nUhPfMOdad6jsHc7Sl_mfCVJFJ7M5TC", true),
            "somethingabout" => ("PL_nUhPfMOdacbmxUn0m8bZf0Gu6SIsTgg", true),
            "allcartoons" => ("PL3A7CB3AB20462EED", true),
            "zeldaoot" => ("PL_nUhPfMOdafrG_2mkNNNwZXYqGXu1Oci", true),

            "sonic1" => ("nTDU9fSMNyI", false),
            "sonic2" => ("4mmV3TpYCwc", false),
            "yoshisisland" => ("Dk8Qy7L5ZaI", false),
            "smashbros" => ("c3ls2y_2UHM", false),
            "kirbysuperstar" => ("O8SL3VEQEqQ", false),
            "mario64" => ("ff9ldkz1h1M", false),
            "donkeykongcountry" => ("kDEBu5orzQU", false),
            "starfox64" => ("5KIW8kZnFAg", false),
            "zeldabotw" => ("1or3YILu28M", false),
            "supermarioworld" => ("_bE3PCRHq-A", false),
            "kirbysadventure" => ("fC3pEQB3_Tc", false),
            "dittosdaycare" => ("JcuVqiwsiGY", false),
            "supermetroid" => ("YcppnDs2Rz8", false),
            "yoshisstory" => ("uyKP3U1824s", false),
            "kirbyamazingmirror" => ("8JzyquUk1NY", false),
            "supermarioallstars" => ("EZQLVKN2SU4", false),
            "smashbrossubspaceemissary" => ("5juBmmXoW7I", false),
            "kirby64" => ("EY14vhBJMKg", false),
            "kirbysdreambuffet" => ("ckwtQLlXZYQ", false),
            "pokemonlegendsarceus" => ("Jr7pjruyR9s", false),
            _ => (string.Empty, false),
        };

        return vidCode is { Length: > 0 }
            ? playlist
                ? $"{vidPlaylistPrefix}{vidCode}"
                : $"{vidCodePrefix}{vidCode}"
            : terminalMontageYouTube;
    }
}
