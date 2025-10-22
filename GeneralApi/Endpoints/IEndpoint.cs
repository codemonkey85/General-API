namespace GeneralApi.Endpoints;

public interface IEndpoint
{
    // ReSharper disable once UnusedMethodReturnValue.Global
    // ReSharper disable once UnusedMemberInSuper.Global
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder app);
}
