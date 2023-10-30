using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.Models.Inmueble;
using ShopApp.Services;
using ShopApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ShopApp.ViewModels;

public partial class BookmarkVM : GlobalVM
{
    [ObservableProperty]
    ObservableCollection<InmuebleResponse> inmuebles;

    [ObservableProperty]
    InmuebleResponse inmuebleSeleccionado;

    readonly INavegacionService _navegacionService;
    readonly InmuebleService _inmuebleService;

    public Command GetInmueblesCommand { get; }

    public BookmarkVM(
        INavegacionService navegacionService,
        InmuebleService inmuebleService)
    {
        _navegacionService = navegacionService;
        _inmuebleService = inmuebleService;
        GetInmueblesCommand = new Command(async () => await LoadDataAsync());
        PropertyChanged += OnPropertyChanged;
    }

    private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(InmuebleSeleccionado))
        {
            var uri = $"{nameof(InmuebleDetailPage)}?id={InmuebleSeleccionado.Id}";
            await _navegacionService.GoToAsync(uri);
        }
    }

    public async Task LoadDataAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var listInmuebles = await _inmuebleService.GetBookmarks();
            Inmuebles = new ObservableCollection<InmuebleResponse>(listInmuebles);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
