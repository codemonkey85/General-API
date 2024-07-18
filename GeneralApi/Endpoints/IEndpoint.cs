namespace GeneralApi.Endpoints;

public interface IEndpoint
{
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder app);
}
