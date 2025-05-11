using Microsoft.Data.SqlClient;
using Dapper;
using Basis.CadastroLivros.Data.Repositories.Interfaces;
using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Data.Repositories;

public class RelatorioRepository : BaseRepository, IRelatorioRepository
{
    public async Task<IEnumerable<VwLivrosComAutores>> GetVwLivrosComAutoresAsync()
    {
        await using SqlConnection connection = new(stringConnection);

        var sql = $"SELECT * FROM dbo.vw_LivrosComAutores";

        return await connection.QueryAsync<VwLivrosComAutores>(sql);
    }
}
