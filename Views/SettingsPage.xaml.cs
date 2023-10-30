using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsVM vM)
	{
		InitializeComponent();
		BindingContext = vM;
	}
}