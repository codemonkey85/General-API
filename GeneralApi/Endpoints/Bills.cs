namespace GeneralApi.Endpoints;

public class Bills : IEndpoint
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder apiGroup)
    {
        var billsGroup = apiGroup.MapGroup("/bills");

        billsGroup.MapPost("/",
            async (HttpContext context, [FromBody] List<BillObject> bills) => await FormatBills(context, bills));

        return apiGroup;
    }

    private static async Task<IResult> FormatBills(HttpContext context, List<BillObject> bills)
    {
        if (bills.Count == 0)
        {
            return TypedResults.NotFound("No bills available.");
        }

        var billsTouse = bills.Where(bill => !string.IsNullOrEmpty(bill.DueDate)).ToList();

        var componentResult = new RazorComponentResult(typeof(BillsView),
            new Dictionary<string, object?> { { nameof(BillsView.Bills), billsTouse } });

        // Use the RazorComponentResult to render and write directly to the response
        await componentResult.ExecuteAsync(context);

        // Return a completed response
        return TypedResults.Ok();
    }
}
