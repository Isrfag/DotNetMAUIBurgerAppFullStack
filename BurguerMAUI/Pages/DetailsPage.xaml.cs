using BurguerMAUI.ViewModels;
using System.Globalization;

namespace BurguerMAUI.Pages;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel detailsViewModel)
	{
		InitializeComponent();
		BindingContext = detailsViewModel;
	}
}