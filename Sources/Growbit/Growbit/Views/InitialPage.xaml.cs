using Growbit.ViewModels;

namespace Growbit.Views;

public partial class InitialPage : ContentPage
{
    private readonly BaseViewModel _viewModel;
    
    public InitialPage(InitialPageViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        BindingContext = _viewModel;
    }

    override protected async void OnAppearing()
    {
       await _viewModel.OnNavigatedTo();
    }
}