namespace GeneralApi;

public static class EndpointBuilder
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app) =>
        app.MapHelloEndpoints();
}
