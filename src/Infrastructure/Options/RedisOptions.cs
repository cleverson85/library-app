namespace Infrastructure.Options;

public class RedisOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public bool Ssl { get; set; }
}
