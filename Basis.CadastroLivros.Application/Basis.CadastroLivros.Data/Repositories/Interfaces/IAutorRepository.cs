using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Data.Repositories.Interfaces;

public interface IAutorRepository
{
    Task<IEnumerable<Autor>> GetItemsAsync();
    Task<Autor> GetItemByIdAsync(int codAu);
    Task<int> Insert(Autor item);
    Task Update(int codAu, Autor item);
    Task Delete(int codAu);
}
