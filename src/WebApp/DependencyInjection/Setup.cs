using Scrutor;

namespace WebApp.DependencyInjection;

public static class Setup
{
    public static void SetupInjection(this IServiceCollection services)
    {
        services.Scan(selector => selector
                .FromAssemblies(Application.AssemblyReference.Assembly, Domain.AssemblyReference.Assembly, Infrastructure.AssemblyReference.Assembly)
                .AddClasses(false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .WithScopedLifetime());

        OptionsPattern.Register(services);
        DbContext.Register(services);
        Swagger.Register(services);
        Versioning.Register(services);
        Authentication.Register(services);
    }
}
