using Dapper.Contrib.Extensions;

namespace Basis.CadastroLivros.Models.Entities;

[Table("dbo.Livro_Preco_Venda")]
public class Livro_Preco_Venda
{
	[ExplicitKey]
	public int Livro_Codl { get; set; }
	[ExplicitKey]
	public int Canal_De_Venda_Cod_Tpv { get; set; }
    public decimal Preco_De_Venda { get; set; }
}
