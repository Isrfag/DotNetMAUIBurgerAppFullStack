using BurguerMAUI.ViewModels;

namespace BurguerMAUI.Pages;

public partial class HomePage : ContentPage
{
	private HomeViewModel viewModel;
	public HomePage(HomeViewModel homeViewModel)
	{
		InitializeComponent();
		viewModel = homeViewModel;
		BindingContext = viewModel;
	}

    protected async override void OnAppearing()
    {
        await viewModel.InitializeAsync();
    }
}