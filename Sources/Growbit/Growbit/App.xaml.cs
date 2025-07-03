using Growbit.Services;
using Growbit.Views;

namespace Growbit;

public partial class App : Application, IAppProvider
{
    public static IServiceProvider Services { get; set; }
    
    public App(IServiceProvider services)
    {
        Services = services;
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var initialPage = Services.GetService(typeof(InitialPage)) as InitialPage;
        return new Window(new NavigationPage(initialPage));
    }

    public INavigation? GetNavigation()
    {
        var last = Windows.FirstOrDefault().Page as NavigationPage;
        return last?.Navigation;
    }
}