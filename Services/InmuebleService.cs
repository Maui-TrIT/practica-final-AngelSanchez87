using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopApp.Models.Config;
using ShopApp.Models.Inmueble;
using System.Net.Http.Headers;
using System.Text;

namespace ShopApp.Services;

public class InmuebleService
{
    private readonly HttpClient _client;
    private readonly Settings _settings;

    public InmuebleService(
        HttpClient client,
        IConfiguration configuration)
    {
        _client = client;
        _settings = configuration.GetRequiredSection(nameof(Settings)).Get<Settings>();
    }

    public async Task<List<CategoryResponse>> GetCategories()
    {
        var uri = $"{_settings.UrlBase}/api/category";
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

        var result = await _client.GetStringAsync(uri);
        return JsonConvert.DeserializeObject<List<CategoryResponse>>(result);
    }

    public async Task<List<InmuebleResponse>> GetInmueblesByCategory(int categoryId)
    {
        var uri = $"{_settings.UrlBase}/api/inmueble/category/{categoryId}";
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

        var result = await _client.GetStringAsync(uri);
        return JsonConvert.DeserializeObject<List<InmuebleResponse>>(result);
    }

    public async Task<List<InmuebleResponse>> GetInmueblesFavoritos()
    {
        var uri = $"{_settings.UrlBase}/api/inmueble/trending";
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

        var result = await _client.GetStringAsync(uri);
        return JsonConvert.DeserializeObject<List<InmuebleResponse>>(result);
    }

    public async Task<InmuebleResponse> GetInmuebleById(int inmuebleId)
    {
        var uri = $"{_settings.UrlBase}/api/inmueble/{inmuebleId}";
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

        var result = await _client.GetStringAsync(uri);
        return JsonConvert.DeserializeObject<InmuebleResponse>(result);
    }

    public async Task<bool> SaveBookmark(BookmarkRequest request)
    {
        var uri = $"{_settings.UrlBase}/api/bookmark";
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

        var response = await _client.PostAsync(uri, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<InmuebleResponse>> GetBookmarks()
    {
        var uri = $"{_settings.UrlBase}/api/inmueble/bookmark";
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

        var result = await _client.GetStringAsync(uri);
        return JsonConvert.DeserializeObject<List<InmuebleResponse>>(result);
    }

    public async Task<List<InmuebleResponse>> GetBusquedaInmuebles(string searchText)
    {
        var uri = $"{_settings.UrlBase}/api/inmueble/search/{searchText}";
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));

        var result = await _client.GetStringAsync(uri);
        return JsonConvert.DeserializeObject<List<InmuebleResponse>>(result);
    }
}
