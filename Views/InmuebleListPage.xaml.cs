using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class InmuebleListPage : ContentPage
{
	public InmuebleListPage(InmuebleListVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}