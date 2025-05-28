namespace GeneralApi.Endpoints;

public class Now : IEndpoint
{
    private static List<NowThing> nowThings = [];

    private const string NowThingsFile = "nowthings.json";

    public Now() => LoadNowThings();

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder app)
    {
        var nowGroup = app.MapGroup("/now");

        nowGroup.MapGet("/", GetNowThings);
        nowGroup.MapPost("/reload", ReloadNowThings);

        return app;
    }

    private static List<NowThing> GetNowThings() => nowThings;

    private static IResult ReloadNowThings()
    {
        LoadNowThings();
        return Results.Ok(nowThings);
    }

    private static void LoadNowThings()
    {
        if (File.Exists(NowThingsFile))
        {
            var json = File.ReadAllText(NowThingsFile);
            var items = JsonSerializer.Deserialize<List<NowThing>>(json);
            if (items is not null)
            {
                nowThings = items;
            }
        }
    }

    private readonly record struct NowThing
    {
        public string Title { get; init; }

        public string Description { get; init; }

        public string? Url { get; init; }
    }
}
