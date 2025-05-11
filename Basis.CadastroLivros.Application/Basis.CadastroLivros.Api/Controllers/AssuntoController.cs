using Microsoft.AspNetCore.Mvc;
using Basis.CadastroLivros.Data.Repositories.Interfaces;
using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssuntoController(
    IAssuntoRepository assuntoRepository,
    ILogger<AssuntoController> logger) : ControllerBase
{
    private readonly IAssuntoRepository assuntoRepository = assuntoRepository;
    private readonly ILogger<AssuntoController> logger = logger;


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Assunto>>> GetItemsAsync()
    {
        try
        {
            var items = await assuntoRepository.GetItemsAsync();
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


    [HttpGet("{codAs:int}")]
    public async Task<ActionResult<Assunto>> GetItemByIdAsync(int codAs)
    {
        try
        {
            var item = await assuntoRepository.GetItemByIdAsync(codAs);

            return Ok(item);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost]
    public async Task<ActionResult<int>> PostItem([FromBody] Assunto item)
    {
        try
        {
            var newItem = await assuntoRepository.Insert(item);

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


    [HttpPut("{codAs:int}")]
    public async Task<ActionResult> PutItem(int codAs, [FromBody]
                                    Assunto item)
    {
        try
        {
            if (codAs == 0)
            {
                return BadRequest(); //Status 400
            }

            await assuntoRepository.Update(codAs, item);
            
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    

    [HttpDelete("{codAs:int}")]
    public async Task<ActionResult> DeleteItem(int codAs)
    {
        try
        {
            if (codAs == 0)
            {
                return BadRequest(); //Status 400
            }
            
            await assuntoRepository.Delete(codAs);
            
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
