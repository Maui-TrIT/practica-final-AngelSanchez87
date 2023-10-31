namespace ShopApp.Models.Backend.Login;

public class UsuarioRegisterRequest
{    
    public string Nombre { get; set; }
    
    public string Apellido { get; set; }

    public string Email { get; set; }

    public string Username { get; set; }

    public string Telefono { get; set; }

    public string Password { get; set; }
}
