using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}