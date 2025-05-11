using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Basis.CadastroLivros.Models.Entities;
using Basis.CadastroLivros.Web.Services.Interfaces;

namespace Basis.CadastroLivros.Web.Services;

public class AssuntoService(HttpClient http,
    ILogger<AssuntoService> logger) : IAssuntoService
{
    private readonly HttpClient _http = http;
    private readonly ILogger<AssuntoService> _logger = logger;

    public async Task<IEnumerable<Assunto>> GetItemsAsync()
    {
        try
        {
            var items = await _http.GetFromJsonAsync<IEnumerable<Assunto>>($"api/assunto");
                                    
            return items ?? default!;
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao obter a lista de assuntos: {Error}", ex.Message);
            throw new ApplicationException("Erro ao obter a lista de assuntos", ex);
        }
    }

    public async Task<Assunto> GetItemByIdAsync(int codl)
    {
        try
        {
            var response = await _http.GetAsync($"api/assunto/{codl}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default!;
                }
                return await response.Content.ReadFromJsonAsync<Assunto>() ?? default!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao obter o assunto: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao obter o assunto: {Error}", ex.Message);
            throw new ApplicationException("Erro ao obter o assunto", ex);
        }
    }


    public async Task<int> Insert(Assunto item)
    {
        try
        {
            var json = JsonSerializer.Serialize(item);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync("api/assunto", requestContent);

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
                _logger.LogError("Erro ao inserir o assunto: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao inserir um assunto: {Error}", ex.Message);
            throw new ApplicationException("Ocorreu um erro ao inserir o assunto", ex);
        }
    }

    public async Task Update(Assunto item)
    {
        try
        {
            var json = JsonSerializer.Serialize(item);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PutAsync($"api/assunto/{item.CodAs}", requestContent);

            if (!response.IsSuccessStatusCode) //status code entre 200 a 299
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao atualizar o assunto: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao inserir assunto: {Error}", ex.Message);
            throw new ApplicationException("Erro ao inserir assunto", ex);
        }
    }
    
    public async Task Delete(int codl)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/assunto/{codl}");

            if (!response.IsSuccessStatusCode) //status code entre 200 a 299
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao deletar o assunto: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao deletar o assunto: {Error}", ex.Message);
            throw new ApplicationException("Erro ao deletar assunto", ex);
        }
    }
}
