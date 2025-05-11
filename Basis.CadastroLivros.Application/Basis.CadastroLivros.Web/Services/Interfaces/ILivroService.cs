using Basis.CadastroLivros.Models.Entities;
using Basis.CadastroLivros.Models.Model;

namespace Basis.CadastroLivros.Web.Services.Interfaces;

public interface ILivroService
{
    Task<IEnumerable<Livro>> GetItemsAsync();
    Task<LivroModel> GetItemByIdAsync(int codAu);
    Task<int> Insert(LivroModel item);
    Task Update(LivroModel item);
    Task Delete(int codl);
}
