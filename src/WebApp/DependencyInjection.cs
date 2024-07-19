using Application.Options;
using Asp.Versioning;
using Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalEndpoints.Abstractions;
using MongoDB.Driver;
using Scrutor;
using StackExchange.Redis;
using System.Reflection;
using System.Text;
using WebApp.OpenApi;

namespace WebApp;

public static class DependencyInjection
{
    public static void Setup(this IServiceCollection services, Assembly assembly)
    {
        ConfigureCors(services);
        AddVersioningApi(services);
        AddRedis(services);
        AddDbContext(services);
        AddServices(services);
        AddEndpoints(services, assembly);
    }


    public static void AddJwtAuthentication(IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsSetup>();

        var jwtOptions = services.BuildServiceProvider()
                                      .GetService<IOptions<JwtOptions>>()!.Value;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();
    }

    public static void ConfigureCors(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "AllowOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });
    }

    public static void AddGenSwagger(IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureSwaggerGenOptions>();

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Please enter JWT with Bearer into field",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                   Array.Empty<string>()
                }
            });
        });
    }

    public static void AddVersioningApi(IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
    }

    public static void AddRedis(IServiceCollection services)
    {
        services.ConfigureOptions<RedisOptionsSetup>();

        var redisOptions = services.BuildServiceProvider().GetService<IOptions<RedisOptions>>()!.Value;
        services.AddSingleton<IConnectionMultiplexer>(options => ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { redisOptions.ConnectionString },
            AbortOnConnectFail = false,
            Ssl = redisOptions.Ssl
        }));
    }

    public static void AddDbContext(IServiceCollection services)
    {
        services.ConfigureOptions<DbOptionsSetup>();

        var databaseOptions = services.BuildServiceProvider()
                                      .GetService<IOptions<DbOptions>>()!.Value;

        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(databaseOptions.ConnectionString);
        });
    }

    public static void AddServices(IServiceCollection services)
    {
        services.Scan(selector => selector
                .FromAssemblies(Application.AssemblyReference.Assembly, Domain.AssemblyReference.Assembly, Infrastructure.AssemblyReference.Assembly)
                .AddClasses(false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .WithScopedLifetime());
    }

    public static void AddEndpoints(IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
}
