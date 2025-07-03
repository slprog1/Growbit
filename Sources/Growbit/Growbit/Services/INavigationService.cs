using Growbit.ViewModels;

namespace Growbit.Services;

public interface INavigationService
{
    Task PushAsync<T>() where T : BaseViewModel;
    Task PushModalAsync<T>() where T : BaseViewModel;
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

    public NavigationService(IServiceProvider serviceProvider, Dictionary<Type, Type> mappings,
        IAppProvider appProvider)
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
        if (page is ContentPage contentPage)
            await _appProvider.GetNavigation()?.PushAsync(contentPage);
    }

    public async Task PushModalAsync<T>() where T : BaseViewModel
    {
        var vmType = typeof(T);

        if (!_mappings.TryGetValue(vmType, out var pageType))
            throw new InvalidOperationException($"No page found for {vmType}");

        var page = (Page)_serviceProvider.GetRequiredService(pageType);
        if (page is ContentPage contentPage)
            await _appProvider.GetNavigation()?.PushModalAsync(contentPage);
    }

    public async Task PopAsync()
    {
        await _appProvider.GetNavigation()?.PopAsync();
    }

    public async Task<TResult> ShowModalAsync<TViewModel, TResult>() where TViewModel : BaseViewModel
    {
        var vmType = typeof(TViewModel);

        if (!_mappings.TryGetValue(vmType, out var pageType))
            throw new InvalidOperationException($"No page found for {vmType}");

        var tcs = new TaskCompletionSource<TResult>();

        var page = (Page)_serviceProvider.GetRequiredService(pageType);

        if (page.BindingContext is TViewModel viewModel)
        {
            if (viewModel is IModalPageViewModel<TResult> modalViewModel)
            {
                modalViewModel.SetResultSource(tcs);

                // Push the modal page
                await _appProvider.GetNavigation()?.PushModalAsync(page);

                var result = await tcs.Task;
                return result;
            }
            else
            {
                throw new InvalidOperationException(
                    $"ViewModel {vmType.Name} does not support returning a result of type {typeof(TResult).Name}. It must implement IModalPageViewModel<TResult> or similar.");
            }
        }
        else
        {
            throw new InvalidOperationException(
                $"The BindingContext of page {pageType.Name} is not of type {vmType.Name}.");
        }
    }
}

public interface IModalPageViewModel<TResult>
{
    void SetNavigationResult(TResult result);
    void SetResultSource(TaskCompletionSource<TResult> tcs);
}