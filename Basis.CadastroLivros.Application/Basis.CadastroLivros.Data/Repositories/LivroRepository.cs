using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Dapper;
using Basis.CadastroLivros.Data.Repositories.Interfaces;
using Basis.CadastroLivros.Models.Entities;
using Basis.CadastroLivros.Models.Model;
using Basis.CadastroLivros.Models.Extensions;

namespace Basis.CadastroLivros.Data.Repositories;

public class LivroRepository : BaseRepository, ILivroRepository
{
    public async Task<IEnumerable<Livro>> GetItemsAsync()
    {
        await using SqlConnection connection = new(stringConnection);

        var sql = $"SELECT * FROM Livro";

        return await connection.QueryAsync<Livro>(sql);
    }


    public async Task<LivroModel> GetItemByIdAsync(int codl)
    {
        await using SqlConnection connection = new(stringConnection);

        var livro = await connection.GetAsync<Livro>(codl);

        LivroModel livroModel = default!;

        if (livro != null)
        {
            livroModel = livro.MapToModel();

            //obter os autores do livro
            var autores = await GetLivroAutoresAsync(codl, connection);
            if (autores != null)
            {
                livroModel.Autores = [.. autores.Select(a => a.Autor_CodAu)];
            }

            //obter os assuntos do livro
            var assuntos = await GetLivroAssuntosAsync(codl, connection);
            if (assuntos != null)
            {
                livroModel.Assuntos = [.. assuntos.Select(a => a.Assunto_CodAs)];
            }

            //obter os preços de venda
            var precosVenda = await GetLivroPrecoVendaAsync(codl, connection);
            if (precosVenda != null)
            {
                livroModel.PrecosVenda = [.. precosVenda];
            }
        }

        return livroModel;
    }


    private static async Task<IEnumerable<Livro_Autor>> GetLivroAutoresAsync(int codl, SqlConnection connection)
    {
        const string sql = "SELECT Livro_Codl, Autor_CodAu FROM Livro_Autor WHERE Livro_Codl = @Codl";
        return await connection.QueryAsync<Livro_Autor>(sql, new { Codl = codl });
    }

    private static async Task<IEnumerable<Livro_Assunto>> GetLivroAssuntosAsync(int codl, SqlConnection connection)
    {
        const string sql = "SELECT Livro_Codl, Assunto_CodAs FROM Livro_Assunto WHERE Livro_Codl = @Codl";
        return await connection.QueryAsync<Livro_Assunto>(sql, new { Codl = codl });
    }

    private static async Task<IEnumerable<Livro_Preco_Venda>> GetLivroPrecoVendaAsync(int codl, SqlConnection connection)
    {
        const string sql = @"
        SELECT 
            Livro_Codl, Canal_De_Venda_Cod_Tpv, Preco_De_Venda 
        FROM Livro_Preco_Venda 
        WHERE Livro_Codl = @Codl";
        return await connection.QueryAsync<Livro_Preco_Venda>(sql, new { Codl = codl });
    }


    public async Task<int> Insert(LivroModel item)
    {
        await using var connection = new SqlConnection(stringConnection);
        await connection.OpenAsync();
        
        using var transaction = connection.BeginTransaction();

        try
        {
            // Insere e pega o novo ID
            var sql = $@"
                INSERT INTO Livro (Titulo, Editora, Edicao, AnoPublicacao)
                VALUES (@Titulo, @Editora, @Edicao, @AnoPublicacao);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            var newCod = await connection.ExecuteScalarAsync<int>(sql, item, transaction);

            //Insere os autores
            await InsertLivroAutoresAsync(newCod, item.Autores, connection, transaction);

            // Inserir os assuntos
            await InsertLivroAssuntosAsync(newCod, item.Assuntos, connection, transaction);

            // Inserir os preços de venda
            await InsertLivroPrecosVendaAsync(newCod, item.PrecosVenda, connection, transaction);

            // Confirma a transação
            await transaction.CommitAsync();

            return newCod;
        }
        catch
        {
            // Reverte a transação se algo falhar
            await transaction.RollbackAsync();
            throw;
        }
    }
    

    private static async Task InsertLivroAutoresAsync(int codl, List<int> autores, 
                                        SqlConnection connection, SqlTransaction transaction)
    {
        //Inserir os autores
        foreach (var codAu in autores)
        {

            var livroAutor = new Livro_Autor
            {
                Livro_Codl = codl,
                Autor_CodAu = codAu
            };

            await connection.InsertAsync(livroAutor, transaction);
        }
    }

    private static async Task DeletarAutoresAsync(int codl, 
                                        SqlConnection connection, SqlTransaction transaction)
    {
        // Deletar os Autores
        const string sqlDeletar = "DELETE FROM Livro_Autor WHERE Livro_Codl = @Livro_Codl";
        await connection.ExecuteAsync(sqlDeletar, new { Livro_Codl = codl }, transaction);
    }


    private static async Task InsertLivroAssuntosAsync(int codl, List<int> assuntos,
                                        SqlConnection connection, SqlTransaction transaction)
    {
        //Inserir o assuntos
        foreach (var codAs in assuntos)
        {

            var livroAssunto = new Livro_Assunto
            {
                Livro_Codl = codl,
                Assunto_CodAs = codAs
            };

            await connection.InsertAsync(livroAssunto, transaction);
        }
    }


    private static async Task InsertLivroPrecosVendaAsync(int codl, List<Livro_Preco_Venda> precosVenda,
                                        SqlConnection connection, SqlTransaction transaction)
    {
        //Inserir o assuntos
        foreach (var precoVenda in precosVenda)
        {
            precoVenda.Livro_Codl = codl;

            await connection.InsertAsync(precoVenda, transaction);
        }
    }


    private static async Task DeletarAssuntosAsync(int codl,
                                        SqlConnection connection, SqlTransaction transaction)
    {
        // Deletar os Assuntos
        const string sqlDeletar = "DELETE FROM Livro_Assunto WHERE Livro_Codl = @Livro_Codl";
        await connection.ExecuteAsync(sqlDeletar, new { Livro_Codl = codl }, transaction);
    }

    private static async Task DeletarPrecosVendaAsync(int codl,
                                        SqlConnection connection, SqlTransaction transaction)
    {
        // Deletar os Assuntos
        const string sqlDeletar = "DELETE FROM Livro_Preco_Venda WHERE Livro_Codl = @Livro_Codl";
        await connection.ExecuteAsync(sqlDeletar, new { Livro_Codl = codl }, transaction);
    }


    public async Task Update(int codl, LivroModel item)
    {
        await using var connection = new SqlConnection(stringConnection);
        await connection.OpenAsync();
        
        using var transaction = connection.BeginTransaction();

        try
        {
            // Atualizar o Livro
            var sqlUpdate = @"
                UPDATE Livro SET
                    Titulo = @Titulo,
                    Editora = @Editora,
                    Edicao = @Edicao,
                    AnoPublicacao = @AnoPublicacao
                WHERE Codl = @Codl
            ";

            await connection.ExecuteAsync(sqlUpdate, new
            {
                Codl = codl,
                item.Titulo,
                item.Editora,
                item.Edicao,
                item.AnoPublicacao
            }, transaction);

            // Deletar os Autores
            await DeletarAutoresAsync(codl, connection, transaction);

            // Inserir os novos Autores
            await InsertLivroAutoresAsync(codl, item.Autores, connection, transaction);


            // Deletar os Assuntos
            await DeletarAssuntosAsync(codl, connection, transaction);

            // Inserir os novos Assuntos
            await InsertLivroAssuntosAsync(codl, item.Assuntos, connection, transaction);


            // Deletar os Preços de Venda
            await DeletarPrecosVendaAsync(codl, connection, transaction);
            
            // Inserir os novos Preços de Venda
            await InsertLivroPrecosVendaAsync(codl, item.PrecosVenda, connection, transaction);


            // Confirma a transação
            await transaction.CommitAsync();
        }
        catch
        {
            // Reverte a transação se algo falhar
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task Delete(int codl)
    {
        await using SqlConnection connection = new(stringConnection);

        var sqlDeleteLivroAutor = $"DELETE FROM Livro_Autor WHERE Livro_Codl = @Livro_Codl ";
        await connection.ExecuteAsync(sqlDeleteLivroAutor, param: new { Livro_Codl = codl } );

        var sqlDeleteLivro = $"DELETE FROM Livro WHERE Codl = @Codl ";
        await connection.ExecuteAsync(sqlDeleteLivro, param: new { Codl = codl } );
    }
}
