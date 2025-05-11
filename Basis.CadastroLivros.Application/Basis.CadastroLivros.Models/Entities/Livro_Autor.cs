using Dapper.Contrib.Extensions;

namespace Basis.CadastroLivros.Models.Entities;

[Table("dbo.Livro_Autor")]
public class Livro_Autor
{
	[ExplicitKey]
	public int Livro_Codl { get; set; }
	[ExplicitKey]
	public int Autor_CodAu { get; set; }
}
