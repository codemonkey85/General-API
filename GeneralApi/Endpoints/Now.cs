namespace GeneralApi.Endpoints;

public class Now : IEndpoint
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder apiGroup)
    {
        var nowGroup = apiGroup.MapGroup("/now");

        nowGroup.MapGet("/", GetNowThings);

        return apiGroup;
    }

    private static async Task<IResult> GetNowThings([AsParameters] NowDependencies deps)
    {
        var appSettings = deps.AppSettings;

        if (appSettings is not { AppKey: { } appKey, BaseId: { } baseId })
        {
            return Results.InternalServerError();
        }

        using var airtableBase = new AirtableBase(appKey, baseId);
        var response = await airtableBase.ListRecords("NowThings");

        if (response.Success)
        {
            List<NowThing> nowThings = [];
            foreach (var record in response.Records)
            {
                nowThings.Add(new NowThing
                {
                    Title = record.GetField<string>("Title") ?? string.Empty,
                    Description = record.GetField<string>("Description") ?? string.Empty,
                    Url = record.GetField<string>("URL")
                });
            }

            return Results.Ok(nowThings);
        }

        return Results.Problem(response.AirtableApiError.Message ?? "Failed to load now things.");
    }

    private sealed class NowDependencies
    {
        public AppSettings AppSettings { get; init; } = default!;
    }

    private readonly record struct NowThing
    {
        public string Title { get; init; }

        public string Description { get; init; }

        public string? Url { get; init; }
    }
}
