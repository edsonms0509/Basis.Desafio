using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Web.Services.Interfaces;

public interface IAutorService
{
    Task<IEnumerable<Autor>> GetItemsAsync();
    Task<Autor> GetItemByIdAsync(int codAu);
    Task<int> Insert(Autor item);
    Task Update(Autor item);
    Task Delete(int codAu);
}
