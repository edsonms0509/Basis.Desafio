using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Web.Services.Interfaces;

public interface IAssuntoService
{
    Task<IEnumerable<Assunto>> GetItemsAsync();
    Task<Assunto> GetItemByIdAsync(int codAs);
    Task<int> Insert(Assunto item);
    Task Update(Assunto item);
    Task Delete(int codAs);
}
