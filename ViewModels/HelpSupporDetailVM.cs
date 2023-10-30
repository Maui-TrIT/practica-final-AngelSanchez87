using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.DataAccess;
using ShopApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShopApp.ViewModels;

public partial class HelpSupporDetailVM : GlobalVM, IQueryAttributable
{
    private readonly IConnectivity _connectivity;
    private readonly CompraService _compraService;
    private readonly ShopOutDbContext _outDbContext;

    [ObservableProperty]
    public ObservableCollection<Compra> compras = new();

    [ObservableProperty]
    private int clientId;

    [ObservableProperty]
    private ObservableCollection<Product> products;

    [ObservableProperty]
    private Product productoSeleccionado;

    [ObservableProperty]
    private int cantidad;

    public HelpSupporDetailVM(
        IConnectivity connectivity,
        CompraService compraService,
        ShopOutDbContext outDbContext)
    {
        _connectivity = connectivity;
        _compraService = compraService;
        _outDbContext = outDbContext;

        _connectivity.ConnectivityChanged += _connectivity_ConnectivityChanged;

        var database = new ShopDbContext();
        Products = new ObservableCollection<Product>(database.Products);
        ProductoSeleccionado = Products.FirstOrDefault();
        //AddCommand = new MiComand(() =>

        // en Maui ya hay clase command
        AddCommand = new Command(() =>
        {
            var compra = new Compra(
                ClientId,
                ProductoSeleccionado.Id,
                Cantidad,
                ProductoSeleccionado.Nombre,
                ProductoSeleccionado.Precio,
                ProductoSeleccionado.Precio * Cantidad);
            Compras.Add(compra);
        },
        () => true
        );
    }

    private void _connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        EnviarCompraCommand.NotifyCanExecuteChanged();
    }

    private bool StatusConnection()
    {
        return _connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
    }

    [RelayCommand(CanExecute = nameof(StatusConnection))]
    private async Task EnviarCompra()
    {
        //Asi se envia a servidor backend-mobile
        //var result = await _compraService.EnviarData(Compras);

        //if (result)
        //{
        //    await Shell.Current.DisplayAlert("Mensaje", "Se enviaron las compras al servidor", "Aceptar");
        //}

        _outDbContext.Database.EnsureCreated();
        foreach (var compra in Compras)
        {
            _outDbContext.Compras.Add(new CompraItem(
                compra.ClientId,
                compra.ProductId,
                compra.Cantidad,
                compra.ProductoPrecio
                ));
        }
        await _outDbContext.SaveChangesAsync();
        await Shell.Current.DisplayAlert("Mensaje", "Se almacenaron las compras en la base de datos", "Aceptar");
    }

    public ICommand AddCommand { get; set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        ClientId = int.Parse(query["id"].ToString());
    }

    [RelayCommand]
    private void EliminarCompra(Compra compra)
    {
        Compras.Remove(compra);
    }
}
