using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Dapper;
using Basis.CadastroLivros.Data.Repositories.Interfaces;
using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Data.Repositories;

public class AssuntoRepository : BaseRepository, IAssuntoRepository
{
    public async Task<IEnumerable<Assunto>> GetItemsAsync()
    {
        await using SqlConnection connection = new(stringConnection);

        var sql = $"SELECT * FROM Assunto";

        return await connection.QueryAsync<Assunto>(sql);
    }

    public async Task<Assunto> GetItemByIdAsync(int codAs)
    {
        await using SqlConnection connection = new(stringConnection);

        return await connection.GetAsync<Assunto>(codAs);
    }

    public async Task<int> Insert(Assunto item)
    {
        await using SqlConnection connection = new(stringConnection);
        await connection.OpenAsync();

        // Insere e pega o novo ID
        var sql = $@"
            INSERT INTO Assunto (Descricao)
            VALUES (@Descricao);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";

        var newCod = await connection.ExecuteScalarAsync<int>(sql, item);

        return newCod;
    }

    public async Task Update(int codAs, Assunto item)
    {
        await using SqlConnection connection = new(stringConnection);

        // Atualizar
        var sqlUpdate = @"
            UPDATE Assunto SET
                Descricao = @Descricao
            WHERE CodAs = @CodAs
        ";
        
        await connection.ExecuteAsync(sqlUpdate, item);
    }

    public async Task Delete(int codAs)
    {
        await using SqlConnection connection = new(stringConnection);

        var sql = $"DELETE FROM Assunto WHERE CodAs = @CodAs ";

        await connection.ExecuteAsync(sql, param: new { CodAs = codAs } );
    }
}
