using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class ProductsPage : ContentPage
{
	public ProductsPage(ProductsVM vm)
	{
		InitializeComponent();
		BindingContext = vm;

		//var dbContext = new ShopDbContext();
		//products.ItemsSource = dbContext.Products;

		//foreach (var item in dbContext.Products)
		//{
		//	//container.Children.Add(new Label { Text = item.Nombre });
		//	var boton = new Button { Text = item.Nombre };
		//	boton.Clicked += async (s, a) =>
		//	{
		//		var uri = $"{nameof(ProductDetailPage)}?id={item.Id}";
		//		await Shell.Current.GoToAsync(uri);
		//	};
		//	container.Children.Add(boton);
		//}
	}
}