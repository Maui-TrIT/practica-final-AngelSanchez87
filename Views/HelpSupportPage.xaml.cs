using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class HelpSupportPage : ContentPage
{
	public HelpSupportPage(HelpSupportVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

  //  private void Button_Clicked(object sender, EventArgs e)
  //  {
		//var dataObject = Resources["data"] as HelpSupportData;
		//dataObject.VisitasPendientes = 30;
  //  }
}