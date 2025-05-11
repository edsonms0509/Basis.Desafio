using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Dapper;
using Basis.CadastroLivros.Data.Repositories.Interfaces;
using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Data.Repositories;

public class CanalVendaRepository : BaseRepository, ICanalDeVendaRepository
{
    public async Task<IEnumerable<Canal_De_Venda>> GetItemsAsync()
    {
        await using SqlConnection connection = new(stringConnection);

        var sql = $"SELECT * FROM Canal_De_Venda";

        return await connection.QueryAsync<Canal_De_Venda>(sql);
    }

    public async Task<Canal_De_Venda> GetItemByIdAsync(int cod_Tpv)
    {
        await using SqlConnection connection = new(stringConnection);

        return await connection.GetAsync<Canal_De_Venda>(cod_Tpv);
    }

    public async Task<int> Insert(Canal_De_Venda item)
    {
        await using SqlConnection connection = new(stringConnection);
        await connection.OpenAsync();

        // Insere e pega o novo ID
        var sql = $@"
            INSERT INTO Canal_De_Venda (Descricao)
            VALUES (@Descricao);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";

        var newCod = await connection.ExecuteScalarAsync<int>(sql, item);

        return newCod;
    }

    public async Task Update(int cod_Tpv, Canal_De_Venda item)
    {
        await using SqlConnection connection = new(stringConnection);
        
        // Atualizar
        var sqlUpdate = @"
            UPDATE Canal_De_Venda SET
                Descricao = @Descricao
            WHERE Cod_Tpv = @Cod_Tpv
        ";
        
        await connection.ExecuteAsync(sqlUpdate, item);
    }

    public async Task Delete(int cod_Tpv)
    {
        await using SqlConnection connection = new(stringConnection);

        var sql = $"DELETE FROM Canal_De_Venda WHERE Cod_Tpv = @Cod_Tpv ";

        await connection.ExecuteAsync(sql, param: new { Cod_Tpv = cod_Tpv } );
    }
}
