using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopApp.Models.Backend.Login;
using ShopApp.Models.Config;
using System.Text;

namespace ShopApp.Services;

public class SecurityService
{
    private HttpClient _client;
    private Settings _settings;

    public SecurityService(HttpClient client,
        IConfiguration configuration)
    {
        _client = client;
        _settings = configuration.GetRequiredSection(nameof(Settings)).Get<Settings>();
    }

    public async Task<bool> Login(string email, string password)
    {
        var url = $"{_settings.UrlBase}/api/usuario/login";
        var request = new LoginRequest { Email = email, Password = password };

        return await SecurityUserRequest(request, url);
    }

    public async Task<bool> RegisterUsuario(
        string nombre,
        string apellido,
        string email,
        string userName,
        string telefono,
        string password)
    {
        var url = $"{_settings.UrlBase}/api/usuario/registrar";
        var request = new UsuarioRegisterRequest
        {
            Nombre = nombre,
            Apellido = apellido,
            Email = email,
            Username = userName,
            Telefono = telefono,
            Password = password 
        };

        return await SecurityUserRequest(request, url);
    }

    private async Task<bool> SecurityUserRequest(object request, string url)
    {
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
            return false;

        var jsonResult = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<UsuarioResponse>(jsonResult);

        Preferences.Set("accesstoken", result.Token);
        Preferences.Set("userid", result.Id);
        Preferences.Set("email", result.Email);
        Preferences.Set("nombre", $"{result.Nombre} {result.Apellido}");
        Preferences.Set("telefono", result.Telefono);
        Preferences.Set("username", result.UserName);

        return true;
    }
}
