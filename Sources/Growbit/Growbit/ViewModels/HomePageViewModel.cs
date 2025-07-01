using CommunityToolkit.Mvvm.Input;
using Growbit.Services;

namespace Growbit.ViewModels;

public partial class HomePageViewModel: BaseViewModel
{
    public HomePageViewModel(INavigationService navigationService) : base(navigationService)
    {
    }

    [RelayCommand]
    private async Task AddNewHabit()
    {
        await NavigationService.PushModalAsync<AddNewHabitPageViewModel>();
    }

    protected internal override async Task OnNavigatedTo()
    {
        await base.OnNavigatedTo();
    }
}