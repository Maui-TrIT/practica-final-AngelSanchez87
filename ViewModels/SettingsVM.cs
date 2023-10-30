﻿using CommunityToolkit.Mvvm.Input;
using ShopApp.Services;
using ShopApp.Views;

namespace ShopApp.ViewModels;

public partial class SettingsVM : GlobalVM
{
    readonly INavegacionService _navegacionService;

    [RelayCommand]
    async Task SalirSesion()
    {
        Preferences.Set("accesstoken", string.Empty);
        var uri = $"//{nameof(AboutPage)}";
        await _navegacionService.GoToAsync(uri);
    }

    public SettingsVM(INavegacionService navegacionService)
    {
        _navegacionService = navegacionService;
    }
}
