const string CorsPolicyName = "AllowAll";

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddCors(options => options.AddPolicy(CorsPolicyName, policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()));

services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddRazorComponents();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseHttpsRedirection();

app.UseCors(CorsPolicyName);

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapAllEndpoints();

app.Run();
