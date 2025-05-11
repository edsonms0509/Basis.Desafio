using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Data.Repositories.Interfaces;

public interface IAssuntoRepository
{
    Task<IEnumerable<Assunto>> GetItemsAsync();
    Task<Assunto> GetItemByIdAsync(int codAs);
    Task<int> Insert(Assunto item);
    Task Update(int codAs, Assunto item);
    Task Delete(int codAs);
}

