var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddRazorComponents();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapAllEndpoints();

app.Run();
