using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.DataAccess;
using System.Collections.ObjectModel;

namespace ShopApp.ViewModels;

public partial class ClientsVM : GlobalVM
{
    [ObservableProperty]
    ObservableCollection<Client> clients;

    [ObservableProperty]
    Client clienteSeleccionado;

    public ClientsVM()
    {
        var dbContext = new ShopDbContext();
        Clients = new ObservableCollection<Client>(dbContext.Clients);
    }
}
