using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class InmuebleBusquedaPage : ContentPage
{
	public InmuebleBusquedaPage(InmuebleBusquedaVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}