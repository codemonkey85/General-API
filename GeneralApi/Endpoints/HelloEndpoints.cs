namespace GeneralApi.Endpoints;

public static class HelloEndpoints
{
    public static IEndpointRouteBuilder MapHelloEndpoints(this IEndpointRouteBuilder apiGroup)
    {
        var helloGroup = apiGroup.MapGroup("/hello");

        helloGroup.MapGet("", HelloWorld);

        helloGroup.MapGet("/name", HelloName);
        helloGroup.MapGet("/name/{name}", (string name) => HelloName(name));

        return apiGroup;
    }

    private static Ok<string> HelloWorld() =>
        TypedResults.Ok("Hello, World!");

    private static Ok<string> HelloName([FromQuery] string name) =>
        TypedResults.Ok($"Hello, {name}!");
}
