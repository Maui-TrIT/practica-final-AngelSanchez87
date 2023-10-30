using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.DataAccess;

namespace ShopApp.ViewModels;

public partial class ResumenVM : GlobalVM
{
    [ObservableProperty]
    int visitas;

    [ObservableProperty]
    int clients;

    [ObservableProperty]
    decimal total;

    [ObservableProperty]
    int totalProducts;

    public ResumenVM(
        ShopOutDbContext outDbContext)
    {
        var db = new ShopDbContext();

        Visitas = outDbContext.Compras
            .ToList()
            .DistinctBy(x => x.ClientId)
            .ToList()
            .Count();

        Clients = db.Clients.Count();

        Total = outDbContext.Compras.ToList().Sum(x => x.Cantidad * x.Precio);

        TotalProducts = outDbContext.Compras.Sum(x => x.Cantidad);
    }
}
