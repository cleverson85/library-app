using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Options;

public class DbOptionsSetup : IConfigureOptions<DbOptions>
{
    private readonly IConfiguration _configuration;
    private const string ConfigurationSectionName = "DatabaseOptions";

    public DbOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(DbOptions options)
    {
        var connecionString = _configuration.GetConnectionString("DefaultConnection");
        options.ConnectionString = connecionString!;

        _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}