using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class BookmarkPage : ContentPage
{
	private BookmarkVM _vm;

	public BookmarkPage(BookmarkVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_vm = vm;
	}

    protected override void OnAppearing()
    {
        _vm.GetInmueblesCommand.Execute(this);
    }
}