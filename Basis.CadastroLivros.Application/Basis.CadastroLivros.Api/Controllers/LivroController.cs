using Microsoft.AspNetCore.Mvc;
using Basis.CadastroLivros.Data.Repositories.Interfaces;
using Basis.CadastroLivros.Models.Entities;
using Basis.CadastroLivros.Models.Model;

namespace Basis.CadastroLivros.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivroController(
    ILivroRepository livroRepository,
    ILogger<LivroController> logger) : ControllerBase
{
    private readonly ILivroRepository livroRepository = livroRepository;
    private readonly ILogger<LivroController> logger = logger;


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Livro>>> GetItemsAsync()
    {
        try
        {
            var items = await livroRepository.GetItemsAsync();
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
    public async Task<ActionResult<LivroModel>> GetItemByIdAsync(int codl)
    {
        try
        {
            var item = await livroRepository.GetItemByIdAsync(codl);

            return Ok(item);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost]
    public async Task<ActionResult<int>> PostItem([FromBody] LivroModel item)
    {
        try
        {
            var newItem = await livroRepository.Insert(item);

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
                                    LivroModel item)
    {
        try
        {
            if (codl == 0)
            {
                return BadRequest(); //Status 400
            }

            await livroRepository.Update(codl, item);
            
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
            
            await livroRepository.Delete(codl);
            
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
