using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Basis.CadastroLivros.Models.Entities;
using Basis.CadastroLivros.Web.Services.Interfaces;

namespace Basis.CadastroLivros.Web.Services;

public class CanalVendaService(HttpClient http,
    ILogger<CanalVendaService> logger) : ICanalVendaService
{
    private readonly HttpClient _http = http;
    private readonly ILogger<CanalVendaService> _logger = logger;

    public async Task<IEnumerable<Canal_De_Venda>> GetItemsAsync()
    {
        try
        {
            var items = await _http.GetFromJsonAsync<IEnumerable<Canal_De_Venda>>($"api/canaldevenda");
                                    
            return items ?? default!;
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao obter a lista de assuntos: {Error}", ex.Message);
            throw new ApplicationException("Erro ao obter a lista de assuntos", ex);
        }
    }

    public async Task<Canal_De_Venda> GetItemByIdAsync(int codl)
    {
        try
        {
            var response = await _http.GetAsync($"api/canaldevenda/{codl}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default!;
                }
                return await response.Content.ReadFromJsonAsync<Canal_De_Venda>() ?? default!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao obter o canal de venda: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao obter o canal de venda: {Error}", ex.Message);
            throw new ApplicationException("Erro ao obter o canal de venda", ex);
        }
    }


    public async Task<int> Insert(Canal_De_Venda item)
    {
        try
        {
            var json = JsonSerializer.Serialize(item);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync("api/canaldevenda", requestContent);

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
                _logger.LogError("Erro ao inserir o canal de venda: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao inserir um canal de venda: {Error}", ex.Message);
            throw new ApplicationException("Ocorreu um erro ao inserir o canal de venda", ex);
        }
    }

    public async Task Update(Canal_De_Venda item)
    {
        try
        {
            var json = JsonSerializer.Serialize(item);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PutAsync($"api/canaldevenda/{item.Cod_Tpv}", requestContent);

            if (!response.IsSuccessStatusCode) //status code entre 200 a 299
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao atualizar o canal de venda: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao inserir canal de venda: {Error}", ex.Message);
            throw new ApplicationException("Erro ao inserir canal de venda", ex);
        }
    }
    
    public async Task Delete(int codl)
    {
        try
        {
            var response = await _http.DeleteAsync($"api/canaldevenda/{codl}");

            if (!response.IsSuccessStatusCode) //status code entre 200 a 299
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao deletar o canal de venda: {Error}", message);
                throw new HttpRequestException($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao deletar o canal de venda: {Error}", ex.Message);
            throw new ApplicationException("Erro ao deletar canal de venda", ex);
        }
    }
}
