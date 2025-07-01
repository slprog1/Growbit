using CommunityToolkit.Mvvm.ComponentModel;
using Growbit.Services;

namespace Growbit.ViewModels;

public class BaseViewModel: ObservableObject
{
    protected INavigationService NavigationService { get; }

    public BaseViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }

    protected internal virtual async Task OnNavigatedTo()
    {
        
    }
}