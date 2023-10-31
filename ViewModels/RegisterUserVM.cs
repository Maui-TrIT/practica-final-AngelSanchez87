using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.Services;
using ShopApp.Views;

namespace ShopApp.ViewModels;

public partial class RegisterUserVM : GlobalVM
{
    readonly SecurityService _securityService;
    readonly INavegacionService _navegacionService;

    [ObservableProperty]
    string nombre;

    [ObservableProperty]
    string apellido;

    [ObservableProperty]
    string email;

    [ObservableProperty]
    string userName;

    [ObservableProperty]
    string telefono;

    [ObservableProperty]
    string password;

    [RelayCommand]
    async Task RegisterUser()
    {
        var result = await _securityService.RegisterUsuario(
            Nombre,
            Apellido,
            Email,
            UserName,
            Telefono,
            Password);

        if (result)
        {
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await Shell.Current.DisplayAlert("Mensaje", "Error en el registro de usuario", "Aceptar");
        }       
    }

    [RelayCommand]
    private async Task OpenLogin()
    {
        var uri = $"{nameof(LoginPage)}";
        await _navegacionService.GoToAsync(uri);
    }

    public RegisterUserVM(
        SecurityService securityService,
        INavegacionService navegacionService)
    {
        _securityService = securityService;
        _navegacionService = navegacionService;
    }
}
