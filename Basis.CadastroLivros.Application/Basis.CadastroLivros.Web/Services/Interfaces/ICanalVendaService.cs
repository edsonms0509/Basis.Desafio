using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Web.Services.Interfaces;

public interface ICanalVendaService
{
    Task<IEnumerable<Canal_De_Venda>> GetItemsAsync();
    Task<Canal_De_Venda> GetItemByIdAsync(int cod_Tpv);
    Task<int> Insert(Canal_De_Venda item);
    Task Update(Canal_De_Venda item);
    Task Delete(int cod_Tpv);
}

