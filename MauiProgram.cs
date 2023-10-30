using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using ShopApp.DataAccess;
using ShopApp.Services;
using ShopApp.ViewModels;
using ShopApp.Views;
using System.Reflection;

namespace ShopApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var assembly = Assembly.GetExecutingAssembly();
		using var stream = assembly.GetManifestResourceStream("ShopApp.appsettings.json");
		var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Configuration.AddConfiguration(config);

		builder.Services.AddSingleton<INavegacionService, NavegacionService>();
		builder.Services.AddTransient<HelpSupportVM>();
		builder.Services.AddTransient<HelpSupportPage>();
        builder.Services.AddTransient<HelpSupporDetailVM>();
        builder.Services.AddTransient<HelpSupportDetailPage>();
        builder.Services.AddTransient<ClientsVM>();
        builder.Services.AddTransient<ClientsPage>();
        builder.Services.AddTransient<ProductsVM>();
        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<ProductDetailVM>();
        builder.Services.AddTransient<ProductDetailPage>();
		builder.Services.AddSingleton(Connectivity.Current);
		builder.Services.AddSingleton<CompraService>();
        builder.Services.AddSingleton<HttpClient>();
		builder.Services.AddSingleton<IDatabaseRutaService, DatabaseRutaService>();
		builder.Services.AddDbContext<ShopOutDbContext>();
        builder.Services.AddTransient<ResumenVM>();
        builder.Services.AddTransient<ResumenPage>();
        builder.Services.AddTransient<LoginVM>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddSingleton<SecurityService>();

        builder.Services.AddTransient<HomeVM>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<BookmarkVM>();
        builder.Services.AddTransient<BookmarkPage>();
        builder.Services.AddTransient<SettingsVM>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<InmuebleListVM>();
        builder.Services.AddTransient<InmuebleListPage>();
        builder.Services.AddSingleton<InmuebleService>();
        builder.Services.AddTransient<InmuebleDetailVM>();
        builder.Services.AddTransient<InmuebleDetailPage>();
        builder.Services.AddTransient<InmuebleBusquedaVM>();
        builder.Services.AddTransient<InmuebleBusquedaPage>();


        var dbContext = new ShopDbContext();
		dbContext.Database.EnsureCreated();
		dbContext.Dispose();

		Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
        Routing.RegisterRoute(nameof(HelpSupportDetailPage), typeof(HelpSupportDetailPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(InmuebleListPage), typeof(InmuebleListPage));
        Routing.RegisterRoute(nameof(InmuebleDetailPage), typeof(InmuebleDetailPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage)); 
        Routing.RegisterRoute(nameof(InmuebleBusquedaPage), typeof(InmuebleBusquedaPage));

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
