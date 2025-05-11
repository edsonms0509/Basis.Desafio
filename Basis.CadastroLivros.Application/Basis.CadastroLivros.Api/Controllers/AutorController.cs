using Microsoft.AspNetCore.Mvc;
using Basis.CadastroLivros.Data.Repositories.Interfaces;
using Basis.CadastroLivros.Models.Entities;

namespace Basis.CadastroLivros.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutorController(
    IAutorRepository autorRepository,
    ILogger<AutorController> logger) : ControllerBase
{
    private readonly IAutorRepository autorRepository = autorRepository;
    private readonly ILogger<AutorController> logger = logger;


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Autor>>> GetItemsAsync()
    {
        try
        {
            var items = await autorRepository.GetItemsAsync();
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


    [HttpGet("{codl:int}")]
    public async Task<ActionResult<Autor>> GetItemByIdAsync(int codl)
    {
        try
        {
            var item = await autorRepository.GetItemByIdAsync(codl);

            return Ok(item);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost]
    public async Task<ActionResult<int>> PostItem([FromBody] Autor item)
    {
        try
        {
            var newItem = await autorRepository.Insert(item);

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


    [HttpPut("{codl:int}")]
    public async Task<ActionResult> PutItem(int codl, [FromBody]
                                    Autor item)
    {
        try
        {
            if (codl == 0)
            {
                return BadRequest(); //Status 400
            }

            await autorRepository.Update(codl, item);
            
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    

    [HttpDelete("{codl:int}")]
    public async Task<ActionResult> DeleteItem(int codl)
    {
        try
        {
            if (codl == 0)
            {
                return BadRequest(); //Status 400
            }
            
            await autorRepository.Delete(codl);
            
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
