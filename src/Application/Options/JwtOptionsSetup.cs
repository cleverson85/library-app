using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Options;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;
    private const string ConfigurationSectionName = "JWTOptions";

    public JwtOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}