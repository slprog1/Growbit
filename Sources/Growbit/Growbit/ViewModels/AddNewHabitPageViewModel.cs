using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Growbit.Models;
using Growbit.Services;

namespace Growbit.ViewModels;

public partial class AddNewHabitPageViewModel: BaseViewModel
{
    public AddNewHabitPageViewModel(INavigationService navigationService) : base(navigationService)
    {
    }

    protected internal override async Task OnNavigatedTo()
    {
        await base.OnNavigatedTo();
    }

    [ObservableProperty] private HabitViewModel _habit;

    [RelayCommand]
    private async Task AddNewHabit()
    {
      // add realm
    }

    [RelayCommand]
    private async Task SelectFrequency()
    {
        // show frequency page viewmodel
    }
}

public partial class HabitViewModel: ObservableObject
{
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _description;
    [ObservableProperty] private FrequencyModel _frequency;
}

public class FrequencyModel
{
    public List<DayOfWeek> Days { get; set; }
    public List<TimeSpan> TimeSpans { get; set; }
}