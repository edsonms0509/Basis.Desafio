using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Data.Repositories.Interfaces;

public interface ICanalDeVendaRepository
{
    Task<IEnumerable<Canal_De_Venda>> GetItemsAsync();
    Task<Canal_De_Venda> GetItemByIdAsync(int cod_Tpv);
    Task<int> Insert(Canal_De_Venda item);
    Task Update(int cod_Tpv, Canal_De_Venda item);
    Task Delete(int cod_Tpv);
}


