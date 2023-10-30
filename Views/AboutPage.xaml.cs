namespace ShopApp.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        var accesToken = Preferences.Get("accesstoken", string.Empty);

        if (string.IsNullOrEmpty(accesToken))
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
    }
}