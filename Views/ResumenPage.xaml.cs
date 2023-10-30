using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class ResumenPage : ContentPage
{
	public ResumenPage(ResumenVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}