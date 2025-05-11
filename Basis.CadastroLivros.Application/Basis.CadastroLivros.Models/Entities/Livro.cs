using Dapper.Contrib.Extensions;

namespace Basis.CadastroLivros.Models.Entities;

[Table("dbo.Livro")]
public class Livro
{
	[Key]
	public int Codl { get; set; }
	public string Titulo { get; set; } = string.Empty;
	public string Editora { get; set; } = string.Empty;
	public int? Edicao { get; set; }
	public string AnoPublicacao { get; set; } = string.Empty;
}
