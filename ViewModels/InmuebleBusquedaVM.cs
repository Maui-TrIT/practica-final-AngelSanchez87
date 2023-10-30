using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.Models.Inmueble;
using ShopApp.Services;
using ShopApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ShopApp.ViewModels;

public partial class InmuebleBusquedaVM : GlobalVM
{
    [ObservableProperty]
    ObservableCollection<InmuebleResponse> inmuebles;

    [ObservableProperty]
    InmuebleResponse inmuebleSeleccionado;

    [ObservableProperty]
    string searchText;

    readonly InmuebleService _inmuebleService;
    readonly INavegacionService _navegacionService;

    [RelayCommand]
    async Task GetBackEvent()
    {
        await _navegacionService.GoToAsync("..");
    }

    public ICommand EjecutarBusqueda => new Command(async () => 
    {
        var inmuebleList = await _inmuebleService.GetBusquedaInmuebles(SearchText);
        Inmuebles = new ObservableCollection<InmuebleResponse>(inmuebleList);
    });

    public InmuebleBusquedaVM(
        InmuebleService inmuebleService,
        INavegacionService navegacionService)
    {
        _inmuebleService = inmuebleService;
        _navegacionService = navegacionService;

        PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(InmuebleSeleccionado))
        {
            var uri = $"{nameof(InmuebleDetailPage)}?id={InmuebleSeleccionado.Id}";
            _navegacionService.GoToAsync(uri);
        }
    }
}
