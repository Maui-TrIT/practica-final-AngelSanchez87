using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.Services;
using ShopApp.Views;

namespace ShopApp.ViewModels;

public partial class LoginVM : GlobalVM
{
    private readonly IConnectivity _connectivity;
    private readonly SecurityService _securityService;
    private readonly INavegacionService _navegacionService;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    public LoginVM(
        IConnectivity connectivity,
        SecurityService securityService,
        INavegacionService navegacionService)
    {
        _connectivity = connectivity;
        _securityService = securityService;
        _navegacionService = navegacionService;

        _connectivity.ConnectivityChanged += _connectivity_ConnectivityChanged;
    }

    private void _connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        LoginMethodCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(StatusConnection))]
    private async Task LoginMethod()
    {
        var result = await _securityService.Login(Email, Password);

        if (result)
        {
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await Shell.Current.DisplayAlert("Mensaje", "Login erroneo", "Aceptar");
        }
    }

    private bool StatusConnection()
    {
        return _connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
    }

    [RelayCommand]
    private async Task OpenRegister()
    {
        var uri = $"{nameof(RegisterUserPage)}";
        await _navegacionService.GoToAsync(uri);
    }
}
