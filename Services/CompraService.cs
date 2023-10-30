using Microsoft.Extensions.Configuration;
using ShopApp.DataAccess;
using ShopApp.Models.Config;
using System.Net.Http.Json;

namespace ShopApp.Services;

public class CompraService
{
    private HttpClient _client;
    private Settings _setings;

    public CompraService(
        HttpClient client,
        IConfiguration configuration)
    {
        _client = client;
        _setings = configuration.GetRequiredSection(nameof(Settings)).Get<Settings>();
    }

    public async Task<bool> EnviarData(IEnumerable<Compra> compras)
    {
        var uri = $"{_setings.UrlBase}/api/compra";
        var body = new { data = compras };

        var result = await _client.PostAsJsonAsync(uri, body);
        return result.IsSuccessStatusCode;
    }
}
