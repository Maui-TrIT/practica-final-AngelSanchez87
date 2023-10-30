using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomeVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}