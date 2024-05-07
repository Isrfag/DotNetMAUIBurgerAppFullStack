using BurguerMAUI.ViewModels;

namespace BurguerMAUI.Pages;

public partial class CartPage : ContentPage
{
	
	public CartPage(CartViewModel cartViewModel)
	{
		InitializeComponent();
		BindingContext = cartViewModel;
	}
}