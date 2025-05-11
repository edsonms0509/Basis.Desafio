using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Web.Services.Interfaces;

public interface IRelatorioService
{
    Task<IEnumerable<VwLivrosComAutores>> GetVwLivrosComAutoresAsync();
}
