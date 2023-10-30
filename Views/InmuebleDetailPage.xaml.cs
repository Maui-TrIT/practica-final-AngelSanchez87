using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class InmuebleDetailPage : ContentPage
{
	public InmuebleDetailPage(InmuebleDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}