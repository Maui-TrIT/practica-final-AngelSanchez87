using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.DataAccess;
using ShopApp.Services;
using ShopApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ShopApp.ViewModels;

public partial class HelpSupportVM : GlobalVM
{
    private readonly INavegacionService _navegacionService;

    [ObservableProperty]
    public int visitasPendientes;

    [ObservableProperty]
    private ObservableCollection<Client> clients;

    [ObservableProperty]
    public Client clienteSeleccionado;
    
    public HelpSupportVM(INavegacionService navegacionService)
    {
        _navegacionService = navegacionService;
        var database = new ShopDbContext();
        Clients = new ObservableCollection<Client>(database.Clients);
        PropertyChanged += HelpSupportData_PropertyChanged;
    }

    private async void HelpSupportData_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ClienteSeleccionado))
        {
            var uri = $"{nameof(HelpSupportDetailPage)}?id={ClienteSeleccionado.Id}";
            await _navegacionService.GoToAsync(uri);
        }
    }
}
