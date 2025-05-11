using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Basis.CadastroLivros.Models.Entities;
using Basis.CadastroLivros.Models.Model;
using Basis.CadastroLivros.Web.Services.Interfaces;

namespace Basis.CadastroLivros.Web.Services;

public class RelatorioService(HttpClient http,
    ILogger<RelatorioService> logger) : IRelatorioService
{
    private readonly HttpClient _http = http;
    private readonly ILogger<RelatorioService> _logger = logger;
    
    public async Task<IEnumerable<VwLivrosComAutores>> GetVwLivrosComAutoresAsync()
    {
        try
        {
            var items = await _http.GetFromJsonAsync<IEnumerable<VwLivrosComAutores>>($"api/relatorio");
            
            return items ?? default!;
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao obter o relatório: {Error}", ex.Message);
            throw new ApplicationException("Erro ao obter o relatório", ex);
        }
    }
}
