namespace WebApp.DependencyInjection;

public static class Setup
{
    public static void SetupInjection(this IServiceCollection services, string corsPolicy)
    {
        OptionsPattern.Register(services);
        Cors.Register(services, corsPolicy);
        DbContext.Register(services);
        Repository.Register(services);
        Operaions.Register(services);
        Swagger.Register(services);
        Versioning.Register(services);
        Authentication.Register(services);
    }
}
