using Asp.Versioning;
using Asp.Versioning.Builder;
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
services.AddSwaggerGen();
services.Setup(typeof(Program).Assembly);
services.AddHealthChecks();
services.AddTransient<GlobalExceptionHandlingMiddleware>();

WebApplication app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoints(versionedGroup);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseCors(CorsPolicy);
app.UseHttpsRedirection();
app.UseHealthChecks(HealthPath);
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.Run();

public partial class Program { }