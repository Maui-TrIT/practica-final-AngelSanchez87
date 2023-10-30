using Microsoft.Maui.Controls.Shapes;
using ShopApp.DataAccess;

namespace ShopApp.Views;

public partial class MainPage : ContentPage
{	
    public MainPage()
	{
		InitializeComponent();

        var dbContext = new ShopDbContext();
        Category.Text = $"{dbContext.Categories.Count()}";
        Client.Text = $"{dbContext.Clients.Count()}";
        Product.Text = $"{dbContext.Products.Count()}";
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        //await DisplayAlert("Mensaje mioo", "Hola!!", "Ok", "cancelar");
        ((Rectangle)sender).ScaleTo(2);
        ((Rectangle)sender).TranslateTo(200, 200);
    }
}

