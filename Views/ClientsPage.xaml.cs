using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class ClientsPage : ContentPage
{
	public ClientsPage(ClientsVM clientsVM)
	{
		InitializeComponent();
        BindingContext = clientsVM;

        //var dbContext = new ShopDbContext();

        //foreach (var item in dbContext.Clients)
        //{
        //    container.Children.Add(new Label { Text = item.Nombre });
        //}
    }
}