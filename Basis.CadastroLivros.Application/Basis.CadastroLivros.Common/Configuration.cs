using Microsoft.Extensions.Configuration;

namespace Basis.CadastroLivros.Common;

public static class Configuration
{
    private static IConfigurationRoot GetConfiguration()
    {
        string c = Directory.GetCurrentDirectory();  
        IConfigurationRoot configuration = new ConfigurationBuilder()
                                                    .SetBasePath(c).AddJsonFile("appSettings.json").Build();
        return configuration;
    }

    public static string? GetConnectionString()
    {
        var configuration = GetConfiguration();
        string? server = configuration.GetSection("SqlConnection:ConnectionString").Value!;
        return server;  
    }
}
