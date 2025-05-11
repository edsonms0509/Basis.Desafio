using Dapper.Contrib.Extensions;

namespace Basis.CadastroLivros.Models.Entities;

[Table("dbo.Livro_Assunto")]
public class Livro_Assunto
{
	[ExplicitKey]
	public int Livro_Codl { get; set; }
	
	[ExplicitKey]
	public int Assunto_CodAs { get; set; }
}
