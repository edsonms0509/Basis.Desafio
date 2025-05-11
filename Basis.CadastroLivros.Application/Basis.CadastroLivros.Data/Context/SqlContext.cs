using Basis.CadastroLivros.Common;

namespace Basis.CadastroLivros.Data.Context;

public static class SqlContext
{
    //BASE 1 - CadastroLivros 
    public static string GetConnectionString()
    {
        var connectionString = Configuration.GetConnectionString();

        return connectionString ?? string.Empty;
    }
}

