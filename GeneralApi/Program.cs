var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapAllEndpoints();

app.Run();
