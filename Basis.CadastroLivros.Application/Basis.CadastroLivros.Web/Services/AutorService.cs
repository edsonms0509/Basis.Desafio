using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Basis.CadastroLivros.Models.Entities;
using Basis.CadastroLivros.Web.Services.Interfaces;

namespace Basis.CadastroLivros.Web.Services;

public class AutorService(HttpClient http,
    ILogger<AutorService> logger) : IAutorService
{
    private readonly HttpClient _http = http;
    private readonly ILogger<AutorService> _logger = logger;

    public async Task<IEnumerable<Autor>> GetItemsAsync()
    {
        try
        {
            var items = await _http.GetFromJsonAsync<IEnumerable<Autor>>($"api/autor");
                                    
            return items ?? default!;
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao obter a lista de autores: {Error}", ex.Message);
            throw new ApplicationException("Erro ao obter a lista de autores", ex);
        }
    }

    public async Task<Autor> GetItemByIdAsync(int codl)
    {
        try
        {
            var response = await _http.GetAsync($"api/autor/{codl}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default!;
                }
                return await response.Content.ReadFromJsonAsync<Autor>() ?? default!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao obter o autor: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao obter o autor: {Error}", ex.Message);
            throw new ApplicationException("Erro ao obter o autor", ex);
        }
    }


    public async Task<int> Insert(Autor item)
    {
        try
        {
            var json = JsonSerializer.Serialize(item);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync("api/autor", requestContent);

            if (response.IsSuccessStatusCode) //status code entre 200 a 299
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default!;
                }
                return await response.Content.ReadFromJsonAsync<int>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao inserir o autor: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao inserir um autor: {Error}", ex.Message);
            throw new ApplicationException("Ocorreu um erro ao inserir o autor", ex);
        }
    }

    public async Task Update(Autor item)
    {
        try
        {
            var json = JsonSerializer.Serialize(item);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PutAsync($"api/autor/{item.CodAu}", requestContent);

            if (!response.IsSuccessStatusCode) //status code entre 200 a 299
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao atualizar o autor: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao inserir autor: {Error}", ex.Message);
            throw new ApplicationException("Erro ao inserir autor", ex);
        }
    }
    
    public async Task Delete(int codl)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/autor/{codl}");

            if (!response.IsSuccessStatusCode) //status code entre 200 a 299
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao deletar o autor: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao deletar o autor: {Error}", ex.Message);
            throw new ApplicationException("Erro ao deletar autor", ex);
        }
    }
}
