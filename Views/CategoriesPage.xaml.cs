using ShopApp.DataAccess;

namespace ShopApp.Views;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage()
	{
		InitializeComponent();

        var dbContext = new ShopDbContext();

        foreach (var item in dbContext.Categories)
        {
            container.Children.Add(new Label { Text = item.Nombre });
        }
    }
}