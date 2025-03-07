﻿namespace GeneralApi.Endpoints;

public class Hello : IEndpoint
{
    private const string World = nameof(World);

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder apiGroup)
    {
        var helloGroup = apiGroup.MapGroup("/hello");

        helloGroup.MapGet("/", HelloName);
        // ReSharper disable once ConvertClosureToMethodGroup
        helloGroup.MapGet("/{name}", (string name) => HelloName(name));

        return apiGroup;
    }

    private static Ok<string> HelloName([FromQuery] string name = World) =>
        TypedResults.Ok($"Hello, {(name is { Length: > 0 } ? name : World)}!");
}
