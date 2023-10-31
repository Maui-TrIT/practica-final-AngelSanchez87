using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class RegisterUserPage : ContentPage
{
	public RegisterUserPage(RegisterUserVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}