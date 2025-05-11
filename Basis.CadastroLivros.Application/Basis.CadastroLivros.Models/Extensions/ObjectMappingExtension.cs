using Basis.CadastroLivros.Models.Entities;
using Basis.CadastroLivros.Models.Model;

namespace Basis.CadastroLivros.Models.Extensions;

public static class ObjectMappingExtension
{
    public static LivroModel MapToModel(this Livro? livro)
    {
        if (livro == null)
            return default!;
        
        return new LivroModel
        {
            Codl = livro.Codl,
            Titulo = livro.Titulo,
            Editora = livro.Editora,
            Edicao = livro.Edicao,
            AnoPublicacao = livro.AnoPublicacao
        };
    }
}
