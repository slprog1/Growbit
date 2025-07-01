using Growbit.Services;
using Growbit.ViewModels;
using Growbit.Views;
using Microsoft.Extensions.Logging;

namespace Growbit;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        var mappings = new Dictionary<Type, Type>();
        RegisterPages(builder.Services, mappings);

        builder.Services.AddSingleton(mappings);
        builder.Services.AddSingleton<IAppProvider>(_ => (App) Application.Current);
        builder.Services.AddTransient<INavigationService, NavigationService>();

        return builder.Build();
    }

    private static void RegisterPages(IServiceCollection builder, Dictionary<Type, Type> mappings)
    {
        builder.RegisterPage<InitialPageViewModel,InitialPage>(mappings);
        builder.RegisterPage<HomePageViewModel, HomePage>(mappings);
    }

    private static void RegisterPage<TPageViewModel, TPage>(this IServiceCollection services,
        Dictionary<Type, Type> mappings)
        where TPageViewModel : BaseViewModel
        where TPage : class
    {
        services.AddTransient<TPageViewModel>();
        services.AddTransient<TPage>();
        mappings.Add(typeof(TPageViewModel), typeof(TPage));
    }

}