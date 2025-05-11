using Microsoft.AspNetCore.Mvc;
using Basis.CadastroLivros.Data.Repositories.Interfaces;
using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CanalDeVendaController(
    ICanalDeVendaRepository canalDeVendaRepository,
    ILogger<CanalDeVendaController> logger) : ControllerBase
{
    private readonly ICanalDeVendaRepository canalDeVendaRepository = canalDeVendaRepository;
    private readonly ILogger<CanalDeVendaController> logger = logger;


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Canal_De_Venda>>> GetItemsAsync()
    {
        try
        {
            var items = await canalDeVendaRepository.GetItemsAsync();
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


    [HttpGet("{cod_Tpv:int}")]
    public async Task<ActionResult<Canal_De_Venda>> GetItemByIdAsync(int cod_Tpv)
    {
        try
        {
            var item = await canalDeVendaRepository.GetItemByIdAsync(cod_Tpv);

            return Ok(item);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost]
    public async Task<ActionResult<int>> PostItem([FromBody] Canal_De_Venda item)
    {
        try
        {
            var newItem = await canalDeVendaRepository.Insert(item);

            if (newItem == 0)
            {
                return NoContent(); //Status 204
            }

            return newItem;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPut("{cod_Tpv:int}")]
    public async Task<ActionResult> PutItem(int cod_Tpv, [FromBody]
                                    Canal_De_Venda item)
    {
        try
        {
            if (cod_Tpv == 0)
            {
                return BadRequest(); //Status 400
            }

            await canalDeVendaRepository.Update(cod_Tpv, item);
            
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    

    [HttpDelete("{cod_Tpv:int}")]
    public async Task<ActionResult> DeleteItem(int cod_Tpv)
    {
        try
        {
            if (cod_Tpv == 0)
            {
                return BadRequest(); //Status 400
            }
            
            await canalDeVendaRepository.Delete(cod_Tpv);
            
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
