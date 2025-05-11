using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Data.Repositories.Interfaces;

public interface IRelatorioRepository
{
    Task<IEnumerable<VwLivrosComAutores>> GetVwLivrosComAutoresAsync();
}
