using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.DataAccess;
using ShopApp.Services;
using ShopApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ShopApp.ViewModels;

public partial class ProductsVM : GlobalVM
{
    private readonly INavegacionService _navegacionService;

    [ObservableProperty]
    ObservableCollection<Product> products;

    [ObservableProperty]
    Product productoSeleccionado;

    [ObservableProperty]
    bool isRefreshing;

    public ProductsVM(INavegacionService navegacionService)
    {
        _navegacionService = navegacionService;
        CargarListaProductos();

        PropertyChanged += ProductsVM_PropertyChanged;
    }

    [RelayCommand]
    private async Task Refresh()
    {
        CargarListaProductos();
        await Task.Delay(3000);
        IsRefreshing = false;
    }

    private async void ProductsVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ProductoSeleccionado))
        {
            var uri = $"{nameof(ProductDetailPage)}?id={ProductoSeleccionado.Id}";
            await _navegacionService.GoToAsync(uri);
        }
    }

    private void CargarListaProductos()
    {
        var dbContext = new ShopDbContext();
        Products = new ObservableCollection<Product>(dbContext.Products);
        dbContext.Dispose(); // no es necesario, se encarga el garbage collector
    }
}
