using Basis.CadastroLivros.Models.Entities;
using Basis.CadastroLivros.Models.Model;

namespace Basis.CadastroLivros.Data.Repositories.Interfaces;

public interface ILivroRepository
{
    Task<IEnumerable<Livro>> GetItemsAsync();
    Task<LivroModel> GetItemByIdAsync(int codl);
    Task<int> Insert(LivroModel item);
    Task Update(int codl, LivroModel item);
    Task Delete(int codl);
}
