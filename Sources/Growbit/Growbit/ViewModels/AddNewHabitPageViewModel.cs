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
}