using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess;
using System.ComponentModel;

namespace ShopApp.ViewModels;

public partial class ProductDetailVM : GlobalVM, IQueryAttributable
{
    [ObservableProperty]
    string nombre;

    [ObservableProperty]
    string description;

    [ObservableProperty]
    decimal precio;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var dbContext = new ShopDbContext();
        var id = int.Parse(query["id"].ToString());
        var producto = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        Nombre = producto.Nombre;
        Description = producto.Description;
        Precio = producto.Precio;
    }
}
