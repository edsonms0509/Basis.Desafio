using Basis.CadastroLivros.Data.Context;

namespace Basis.CadastroLivros.Data.Repositories;

public class BaseRepository
{
    protected readonly string stringConnection = string.Empty;
    protected readonly string stringConnectionBdprom1 = string.Empty;
    
    public BaseRepository()
    {
        stringConnection = SqlContext.GetConnectionString();
    }
}
