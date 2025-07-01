using CommunityToolkit.Mvvm.Input;
using Growbit.Services;

namespace Growbit.ViewModels;

public partial class InitialPageViewModel: BaseViewModel
{
    public InitialPageViewModel(INavigationService navigationService) : base(navigationService)
    {
    }

    [RelayCommand]
    private async Task NavigateToHomePage()
    {
        await NavigationService.PushAsync<HomePageViewModel>();
    }

    protected internal override async Task OnNavigatedTo()
    {
        await base.OnNavigatedTo();

        // this one is just for our cool animation of growing leaves))
        await Task.Delay(2000);
        await NavigateToHomePage();
    }
}