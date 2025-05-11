using Dapper.Contrib.Extensions;

namespace Basis.CadastroLivros.Models.Entities;

[Table("dbo.Assunto")]
public class Assunto
{
	[Key]
	public int CodAs { get; set; }
	public string Descricao { get; set; } = string.Empty;
}
