using Dapper.Contrib.Extensions;

namespace Basis.CadastroLivros.Models.Entities;

[Table("dbo.Autor")]
public class Autor
{
	[Key]
	public int CodAu { get; set; }
	public string Nome { get; set; } = string.Empty;

}
