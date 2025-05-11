using Microsoft.AspNetCore.Components;
using Basis.CadastroLivros.Web.Components;

namespace Basis.CadastroLivros.Web.Pages;

public class CrudBase : ComponentBase
{
    [Parameter]
    public int Id { get; set; }

    protected bool ExibindoSpinner { get; set; }
    protected ExibirErro Erro { get; set; } = default!;
    protected const string paramError = "Não foi possível encontrar a URL solicitada";

    protected void ExibirSpinner()
    {
        ExibindoSpinner = true;
        StateHasChanged();
    }

    protected void OcultarSpinner()
    {
        ExibindoSpinner = false;
        StateHasChanged();
    }
}
