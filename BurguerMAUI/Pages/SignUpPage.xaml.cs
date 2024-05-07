using BurguerMAUI.ViewModels;

namespace BurguerMAUI.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(AuthViewModel authViewModel)
	{
		InitializeComponent();
		BindingContext = authViewModel;
	}

	private async void SignInLabelTapped(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(SignInPage));

	}
}