namespace GeneralApi;

public static class EndpointBuilder
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("", () => Results.Redirect("/swagger"));

        return app
            .MapHelloEndpoints()
            .MapAvmApiEndpoints();
    }
}
