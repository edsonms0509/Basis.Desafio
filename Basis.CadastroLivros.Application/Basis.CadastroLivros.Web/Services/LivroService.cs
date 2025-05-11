using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Basis.CadastroLivros.Models.Entities;
using Basis.CadastroLivros.Models.Model;
using Basis.CadastroLivros.Web.Services.Interfaces;

namespace Basis.CadastroLivros.Web.Services;

public class LivroService(HttpClient http,
    ILogger<LivroService> logger) : ILivroService
{
    private readonly HttpClient _http = http;
    private readonly ILogger<LivroService> _logger = logger;

    public async Task<IEnumerable<Livro>> GetItemsAsync()
    {
        try
        {
            var items = await _http.GetFromJsonAsync<IEnumerable<Livro>>($"api/livro");
                                    
            return items ?? default!;
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao obter a lista de livros: {Error}", ex.Message);
            throw new ApplicationException("Erro ao obter a lista de livros", ex);
        }
    }

    public async Task<LivroModel> GetItemByIdAsync(int codl)
    {
        try
        {
            var response = await _http.GetAsync($"api/livro/{codl}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default!;
                }
                return await response.Content.ReadFromJsonAsync<LivroModel>() ?? default!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao obter o livro: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao obter o livro: {Error}", ex.Message);
            throw new ApplicationException("Erro ao obter o livro", ex);
        }
    }


    public async Task<int> Insert(LivroModel item)
    {
        try
        {
            var json = JsonSerializer.Serialize(item);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync("api/livro", requestContent);

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
                _logger.LogError("Erro ao inserir o livro: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao inserir um livro: {Error}", ex.Message);
            throw new ApplicationException("Ocorreu um erro ao inserir o livro", ex);
        }
    }

    public async Task Update(LivroModel item)
    {
        try
        {
            var json = JsonSerializer.Serialize(item);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PutAsync($"api/livro/{item.Codl}", requestContent);

            if (!response.IsSuccessStatusCode) //status code entre 200 a 299
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao atualizar o livro: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao inserir livro: {Error}", ex.Message);
            throw new ApplicationException("Erro ao inserir livro", ex);
        }
    }
    
    public async Task Delete(int codl)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/livro/{codl}");

            if (!response.IsSuccessStatusCode) //status code entre 200 a 299
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao deletar o livro: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao deletar o livro: {Error}", ex.Message);
            throw new ApplicationException("Erro ao deletar livro", ex);
        }
    }
}
