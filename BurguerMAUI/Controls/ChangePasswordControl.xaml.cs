using BurguerMAUI.ViewModels;
using CommunityToolkit.Maui.Views;

namespace BurguerMAUI.Controls;

public partial class ChangePasswordControl : Popup
{
	public ChangePasswordControl(ChangePasswordViewModel changePasswordViewModel)
	{
		InitializeComponent();
		BindingContext = changePasswordViewModel;
	}

	private async void ClosePopupTab (object sender, TappedEventArgs e) => await CloseAsync();

}