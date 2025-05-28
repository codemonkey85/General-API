namespace GeneralApi.Endpoints;

public class Bills : IEndpoint
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder apiGroup)
    {
        var billsGroup = apiGroup.MapGroup("/bills");

        billsGroup.MapPost("/", async (
                HttpContext context,
                [FromBody] List<BillObject> bills,
                [FromQuery] int daysAhead = 0) =>
            await FormatBills(context, bills, daysAhead));

        return apiGroup;
    }

    private static async Task<IResult> FormatBills(
        HttpContext context,
        List<BillObject> bills,
        int daysAhead = 0)
    {
        if (bills.Count == 0)
        {
            return TypedResults.NotFound("No bills available.");
        }

        var billsToUse = bills
            .Where(bill => bill.DueDateDisplay is not null && (
                daysAhead == 0 ||
                daysAhead > 0 &&
                bill.DueDateDisplay.Value.Date <= DateTime.Today.AddDays(14).Date)
            )
            .OrderBy(bill => bill.DueDateDisplay)
            .ThenBy(bill => bill.Title)
            .ThenByDescending(bill => bill.AmountDueDisplay)
            .ToList();

        var componentResult = new RazorComponentResult(typeof(BillsView),
            new Dictionary<string, object?> { { nameof(BillsView.Bills), billsToUse } });

        // Use the RazorComponentResult to render and write directly to the response
        await componentResult.ExecuteAsync(context);

        // Return a completed response
        return TypedResults.Ok();
    }
}
