namespace BurguerMAUI.Pages;
using BurguerMAUI.ViewModels;

public partial class SignInPage : ContentPage
{
	public SignInPage(AuthViewModel authViewModel)
	{
		InitializeComponent();
		BindingContext = authViewModel;
	}

	private async void SignUpLabelTapped(object sender, EventArgs e )
	{
		await Shell.Current.GoToAsync(nameof(SignUpPage));
	}

	
}