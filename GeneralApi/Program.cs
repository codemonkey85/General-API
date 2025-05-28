var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// 1. Add CORS services and define a policy
services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddRazorComponents();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseHttpsRedirection();

// 2. Use the CORS middleware before mapping endpoints
app.UseCors("AllowAll");

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapAllEndpoints();

app.Run();
