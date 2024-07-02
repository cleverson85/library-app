namespace WebApp.DependencyInjection;

public static class Setup
{
    public static void SetupInjection(this IServiceCollection services)
    {
        OptionsPattern.Register(services);
        DbContext.Register(services);
        Repository.Register(services);
        Operaions.Register(services);
        Versioning.Register(services);
        Swagger.Register(services);
        Authentication.Register(services);
    }
}
