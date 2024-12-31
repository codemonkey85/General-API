using Microsoft.AspNetCore.Components;

namespace GeneralApi.Endpoints;

public partial class BillsView : ComponentBase
{
    [Parameter] public List<BillObject>? Bills { get; set; }
}
