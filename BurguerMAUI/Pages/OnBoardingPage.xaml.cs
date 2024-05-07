using BurguerMAUI.Services;

namespace BurguerMAUI.Pages;

public partial class OnBoardingPage : ContentPage
{
	private readonly AuthService _authService;
	public OnBoardingPage(AuthService authService)
	{
		InitializeComponent();
		_authService = authService;
		
	}

    protected override async void OnAppearing()
    {
		//Si esta logueado ya que no pise esta pagina
        if(_authService.User is not null 
			&& _authService.User.Id != default(Guid) 
			&& !string.IsNullOrWhiteSpace(_authService.Token) )
		{
			//Usuario logueado
			//Navegacion a la pagina principal
			await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
		}
    }

    private async void Clicked_SignIn (object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(SignInPage));
	}

    private async void Clicked_SignUp(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignUpPage));
    }
}