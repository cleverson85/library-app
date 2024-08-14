using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Builder;
using KafkaFlow;
using Serilog;
using WebApp;
using WebApp.Middlewares;

const string CorsPolicy = "AllowOrigin";
const string HealthPath = "/health";

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddMvc();
services.Setup(typeof(Program).Assembly, CorsPolicy);
services.AddAuthorization();
services.AddHealthChecks();
services.AddTransient<GlobalExceptionHandlingMiddleware>();

WebApplication app = builder.Build();

var bus = app.Services.CreateKafkaBus();
await bus.StartAsync();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .HasApiVersion(new ApiVersion(2))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoints(versionedGroup);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

        foreach (ApiVersionDescription description in descriptions)
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseCors(CorsPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.UseHealthChecks(HealthPath);
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.Run();

public partial class Program { }