using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Growbit.ViewModels;

namespace Growbit.Views;

public partial class HomePage : ContentPage
{
    private readonly BaseViewModel _viewModel;
    
    public HomePage(HomePageViewModel viewModel)
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