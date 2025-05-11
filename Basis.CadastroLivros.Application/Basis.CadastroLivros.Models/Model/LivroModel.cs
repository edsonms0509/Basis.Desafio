using System;
using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Models.Model;

public class LivroModel : Livro
{
    public List<int> Autores { get; set; } = [];
    public List<int> Assuntos { get; set; } = [];
    public List<Livro_Preco_Venda> PrecosVenda { get; set; } = [];
}
