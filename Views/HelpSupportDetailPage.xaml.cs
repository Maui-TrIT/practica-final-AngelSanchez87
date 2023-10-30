using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class HelpSupportDetailPage : ContentPage/*, IQueryAttributable*/
{
	public HelpSupportDetailPage(HelpSupporDetailVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

  //  public void ApplyQueryAttributes(IDictionary<string, object> query)
  //  {
  ////      Title = $"Cliente: {query["id"]}";
		////((HelpSupporDetailVM)BindingContext).ClientId = int.Parse(query["id"].ToString());
  //  }
}