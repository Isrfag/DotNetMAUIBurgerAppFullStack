using BurguerMAUI.ViewModels;

namespace BurguerMAUI.Pages;

public partial class ProfilePage : ContentPage
{
	private readonly ProfileViewModel _profileViewModel;
	public ProfilePage(ProfileViewModel profileViewModel)
	{
		InitializeComponent();
		BindingContext = profileViewModel;
		_profileViewModel = profileViewModel;
	}

    protected override void OnAppearing() //Para que pille el nombre nada mas abrirse
    {
        _profileViewModel.Initialize();
    }
}