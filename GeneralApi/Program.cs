const string corsPolicyName = "AllowAll";

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddCors(options => options.AddPolicy(corsPolicyName, policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()));

services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddRazorComponents();

services
    .Configure<AppSettings>(builder.Configuration.GetSection(AppSettings.SectionName))
    .AddSingleton(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value);

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapAllEndpoints();

app.Run();
