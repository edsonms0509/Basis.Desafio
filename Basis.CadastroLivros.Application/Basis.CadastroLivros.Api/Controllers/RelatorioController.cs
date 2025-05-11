using Microsoft.AspNetCore.Mvc;
using Basis.CadastroLivros.Data.Repositories.Interfaces;
using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatorioController(
    IRelatorioRepository relatorioRepository,
    ILogger<RelatorioController> logger) : ControllerBase
{
    private readonly IRelatorioRepository relatorioRepository = relatorioRepository;
    private readonly ILogger<RelatorioController> logger = logger;


    [HttpGet]
    public async Task<ActionResult<IEnumerable<VwLivrosComAutores>>> GetItemsAsync()
    {
        try
        {
            var items = await relatorioRepository.GetVwLivrosComAutoresAsync();
            if (items is null)
            {
                return NotFound();
            }

            return Ok(items);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
