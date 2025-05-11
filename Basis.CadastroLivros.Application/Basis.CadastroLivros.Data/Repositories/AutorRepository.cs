using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Dapper;
using Basis.CadastroLivros.Data.Repositories.Interfaces;
using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Data.Repositories;

public class AutorRepository : BaseRepository, IAutorRepository
{
    public async Task<IEnumerable<Autor>> GetItemsAsync()
    {
        await using SqlConnection connection = new(stringConnection);

        var sql = $"SELECT * FROM Autor";

        return await connection.QueryAsync<Autor>(sql);
    }

    public async Task<Autor> GetItemByIdAsync(int codAu)
    {
        await using SqlConnection connection = new(stringConnection);

        return await connection.GetAsync<Autor>(codAu);
    }

    public async Task<int> Insert(Autor item)
    {
        await using SqlConnection connection = new(stringConnection);
        await connection.OpenAsync();

        // Insere e pega o novo ID
        var sql = $@"
            INSERT INTO Autor (Nome)
            VALUES (@Nome);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";

        var newCod = await connection.ExecuteScalarAsync<int>(sql, item);

        return newCod;
    }

    public async Task Update(int codAu, Autor item)
    {
        await using SqlConnection connection = new(stringConnection);

        // Atualizar
        var sqlUpdate = @"
            UPDATE Autor SET
                Nome = @Nome
            WHERE CodAu = @CodAu
        ";
        
        await connection.ExecuteAsync(sqlUpdate, item);
    }

    public async Task Delete(int codAu)
    {
        await using SqlConnection connection = new(stringConnection);

        var sql = $"DELETE FROM Autor WHERE CodAu = @CodAu ";

        await connection.ExecuteAsync(sql, param: new { CodAu = codAu } );
    }
}
