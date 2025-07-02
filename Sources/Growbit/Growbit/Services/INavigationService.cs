using Growbit.ViewModels;

namespace Growbit.Services;

public interface INavigationService
{
    Task PushAsync<T>() where T: BaseViewModel;
    Task PushModalAsync<T>() where T: BaseViewModel;
    Task PopAsync();
}

public interface IAppProvider
{
    INavigation? GetNavigation();
}

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAppProvider _appProvider;
    private readonly Dictionary<Type, Type> _mappings;

    public NavigationService(IServiceProvider serviceProvider, Dictionary<Type, Type> mappings, IAppProvider appProvider)
    {
        _serviceProvider = serviceProvider;
        _mappings = mappings;
        _appProvider = appProvider;
    }
    
    public async Task PushAsync<T>() where T : BaseViewModel
    {
        var vmType = typeof(T);

        if (!_mappings.TryGetValue(vmType, out var pageType))
            throw new InvalidOperationException($"No page found for {vmType}");

        var page = (Page)_serviceProvider.GetRequiredService(pageType);
        if(page is ContentPage contentPage)
            await _appProvider.GetNavigation()?.PushAsync(contentPage);
    }

    public async Task PushModalAsync<T>() where T : BaseViewModel
    {
        var vmType = typeof(T);

        if (!_mappings.TryGetValue(vmType, out var pageType))
            throw new InvalidOperationException($"No page found for {vmType}");

        var page = (Page)_serviceProvider.GetRequiredService(pageType);
        if(page is ContentPage contentPage)
            await _appProvider.GetNavigation()?.PushModalAsync(contentPage);
    }

    public async Task PopAsync()
    {
        await _appProvider.GetNavigation()?.PopAsync();
    }
}

