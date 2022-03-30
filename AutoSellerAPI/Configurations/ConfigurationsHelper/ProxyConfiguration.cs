using Microsoft.Extensions.Configuration;

namespace Configurations.ConfigurationsHelper;

public static class ProxyConfiguration
{
    public static IConfiguration Use {get; private set; }

    public static void Initialize(IConfiguration config)
    {
        Use = config;
    }
}