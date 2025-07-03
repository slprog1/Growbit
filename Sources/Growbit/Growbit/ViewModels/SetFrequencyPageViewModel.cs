using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Growbit.Services;

namespace Growbit.ViewModels;

public partial class SetFrequencyPageViewModel : BaseViewModel, IModalPageViewModel<FrequencyModel>
{
    private TaskCompletionSource<FrequencyModel> _tcs;

    public SetFrequencyPageViewModel(INavigationService navigationService) : base(navigationService)
    {
        Days = new ObservableCollection<SelectedDayViewModel>
        {
            new SelectedDayViewModel { Day = DayOfWeek.Monday },
            new SelectedDayViewModel { Day = DayOfWeek.Tuesday },
            new SelectedDayViewModel { Day = DayOfWeek.Wednesday },
            new SelectedDayViewModel { Day = DayOfWeek.Thursday },
            new SelectedDayViewModel { Day = DayOfWeek.Friday },
            new SelectedDayViewModel { Day = DayOfWeek.Saturday },
            new SelectedDayViewModel { Day = DayOfWeek.Sunday }
        };
        InitializeTimeSpans(60);
    }

    [ObservableProperty] private ObservableCollection<SelectedDayViewModel> _days;
    [ObservableProperty] private ObservableCollection<SelectedTimeSpanViewModel> _timeSpans;

    [RelayCommand]
    private void UpdateFrequency()
    {
        SetNavigationResult(new FrequencyModel()
        {
            Days = Days.Where(x=>x.IsSelected).Select(x=>x.Day).ToList(), 
            TimeSpans = TimeSpans.Where(x=>x.IsSelected).Select(x=>x.Time).ToList(),
        });
    }

    public void SetNavigationResult(FrequencyModel result)
    {
        _tcs.SetResult(result);
    }

    public void SetResultSource(TaskCompletionSource<FrequencyModel> tcs)
    {
        _tcs = tcs;
    }

    private void InitializeTimeSpans(int minutesStep)
    {
        var tempCollection = new ObservableCollection<SelectedTimeSpanViewModel>();

        //1440 minutes in a day
        for (int i = 0; i < 1440; i += minutesStep)
        {
            tempCollection.Add(new SelectedTimeSpanViewModel(){Time = TimeSpan.FromMinutes(i)});
        }

        TimeSpans = tempCollection; // Assign to the ObservableProperty
    }
}

public partial class SelectedDayViewModel : ObservableObject
{
    [ObservableProperty] private DayOfWeek _day;
    [ObservableProperty] private bool _isSelected;
}

public partial class SelectedTimeSpanViewModel : ObservableObject
{
    [ObservableProperty] private TimeSpan _time;
    [ObservableProperty] private bool _isSelected;
}