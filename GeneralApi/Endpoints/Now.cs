using System.Diagnostics.CodeAnalysis;

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

        if (response is not { Success: true, Records: not null })
        {
            return Results.Problem(response.AirtableApiError?.Message ?? "Failed to load now things.");
        }

        var nowThings = response.Records
            .Select(record => new NowThing
            {
                Title = record.GetField<string>("Title") ?? string.Empty,
                Description = record.GetField<string>("Description") ?? string.Empty,
                Url = record.GetField<string>("URL")
            }).ToList();

        return Results.Ok(nowThings);
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private sealed class NowDependencies
    {
        // ReSharper disable once ReplaceAutoPropertyWithComputedProperty
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public AppSettings AppSettings { get; init; } = null!;
    }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    private readonly record struct NowThing
    {
        public string Title { get; init; }

        public string Description { get; init; }

        public string? Url { get; init; }
    }
}
