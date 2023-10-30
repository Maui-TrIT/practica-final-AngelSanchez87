using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.Models.Inmueble;
using ShopApp.Services;

namespace ShopApp.ViewModels;

public partial class InmuebleDetailVM : GlobalVM, IQueryAttributable
{
    [ObservableProperty]
    InmuebleResponse inmueble;

    [ObservableProperty]
    string imagenSource;

    private readonly INavegacionService _navegacionService;
    private readonly InmuebleService _inmuebleService;

    public InmuebleDetailVM(
        INavegacionService navegacionService,
        InmuebleService inmuebleService)
    {
        _navegacionService = navegacionService;
        _inmuebleService = inmuebleService;
    }

    public async Task LoadDataAsync(int inmuebleId)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            Inmueble = await _inmuebleService.GetInmuebleById(inmuebleId);
            ImagenSource = Inmueble.IsBookmarkEnabled ? "bookmark_fill_icon" : "bookmark_empty_icon";
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

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var id = int.Parse(query["id"].ToString());
        await LoadDataAsync(id);
    }

    [RelayCommand]
    async Task GetBackEvent()
    {
        await _navegacionService.GoToAsync("..");
    }

    [RelayCommand]
    async Task SaveBookmark()
    {
        var bookMark = new BookmarkRequest
        {
            InmuebleId = Inmueble.Id,
            UsuarioId = Preferences.Get("userid", string.Empty)
        };

        await _inmuebleService.SaveBookmark(bookMark);
        await LoadDataAsync(Inmueble.Id);
    }

    [RelayCommand]
    async Task CallOwner()
    {
        var confirmarLlamar = await Application.Current.MainPage.DisplayAlert(
            "Marcar este numero de telefono",
            $"¿Desea llamar a este numero de telefono: {Inmueble.Telefono}?",
            "Si",
            "No");

        if (confirmarLlamar)
        {
            try
            {
                PhoneDialer.Open(Inmueble.Telefono);
            }
            catch (ArgumentNullException) 
            {
                await Application.Current.MainPage
                    .DisplayAlert("No se puede realizar la llamada", "El numero no es valido", "Aceptar");
            }
            catch (FeatureNotEnabledException)
            {
                await Application.Current.MainPage
                    .DisplayAlert("No se puede realizar la llamada", "El dispositivo no soporta llamadas", "Aceptar");
            }
            catch (Exception)
            {
                await Application.Current.MainPage
                    .DisplayAlert("No se puede realizar la llamada", "Errores en la marcación del numero", "Aceptar");
            }
        }
    }

    [RelayCommand]
    async Task TextMessageOwner()
    {
        var message = new SmsMessage("Hola, por favor envíame más informarcion de la vivienda", Inmueble.Telefono);
        var confirmarMensajeTexto = await Application.Current.MainPage.DisplayAlert(
            "Enviar mensaje de texto",
            $"¿Desea enviar un mensaje de texto a este numero de telefono: {Inmueble.Telefono}?",
            "Si",
            "No");

        if (confirmarMensajeTexto)
        {
            try
            {
                await Sms.ComposeAsync(message);
            }
            catch (ArgumentNullException)
            {
                await Application.Current.MainPage
                    .DisplayAlert("No se puede enviar este SMS", "El numero no es valido", "Aceptar");
            }
            catch (FeatureNotEnabledException)
            {
                await Application.Current.MainPage
                    .DisplayAlert("No se puede enviar este SMS", "El dispositivo no soporta envío de SMS", "Aceptar");
            }
            catch (Exception)
            {
                await Application.Current.MainPage
                    .DisplayAlert("No se puede enviar este SMS", "Errores en el envío de SMS", "Aceptar");
            }
        }
    }
}
