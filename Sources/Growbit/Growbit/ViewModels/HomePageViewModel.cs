using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Growbit.Models;
using Growbit.Services;

namespace Growbit.ViewModels;

public partial class HomePageViewModel: BaseViewModel
{
    public HomePageViewModel(INavigationService navigationService) : base(navigationService)
    {
    }
    
    [ObservableProperty]
    ObservableCollection<Habit> _habits = new ();

    [RelayCommand]
    private async Task AddNewHabit()
    {
        await NavigationService.PushAsync<AddNewHabitPageViewModel>();
    }

    [RelayCommand]
    private async Task RemoveHabit(Habit habit)
    {
        
    }

    [RelayCommand]
    private async Task RemoveAllHabits()
    {
        
    }

    [RelayCommand]
    private async Task OpenHabit(Habit habit)
    {
        
    }

    protected internal override async Task OnNavigatedTo()
    {
        await base.OnNavigatedTo();
    }
}