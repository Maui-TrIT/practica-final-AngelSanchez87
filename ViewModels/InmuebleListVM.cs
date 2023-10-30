using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.Models.Inmueble;
using ShopApp.Services;
using ShopApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ShopApp.ViewModels;

public partial class InmuebleListVM : GlobalVM, IQueryAttributable
{
    private readonly INavegacionService _navegacionService;
    private readonly InmuebleService _inmuebleService;

    [ObservableProperty]
    ObservableCollection<InmuebleResponse> inmuebles;

    [ObservableProperty]
    InmuebleResponse inmuebleSeleccionado;

    public InmuebleListVM(
        INavegacionService navegacionService,
        InmuebleService inmuebleService)
    {
        _navegacionService = navegacionService;
        _inmuebleService = inmuebleService;

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

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var id = int.Parse(query["id"].ToString());
        await LoadDataAsync(id);
    }

    public async Task LoadDataAsync(int categoryId)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var listInmuebles = await _inmuebleService.GetInmueblesByCategory(categoryId);
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
