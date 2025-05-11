using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Models.Model;

public class LivroPrecoVendaModel : Livro_Preco_Venda
{
    public string DescricaoCanalVenda { get; set;} = string.Empty;
}