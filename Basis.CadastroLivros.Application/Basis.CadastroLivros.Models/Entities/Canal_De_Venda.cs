using Dapper.Contrib.Extensions;

namespace Basis.CadastroLivros.Models.Entities;

[Table("dbo.Canal_De_Venda")]
public class Canal_De_Venda
{
	[Key]
	public int Cod_Tpv { get; set; }
	public string Descricao { get; set; } = string.Empty;
}
