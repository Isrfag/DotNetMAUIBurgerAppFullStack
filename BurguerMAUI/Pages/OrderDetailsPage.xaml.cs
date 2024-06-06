using BurguerMAUI.ViewModels;

namespace BurguerMAUI.Pages;

public partial class OrderDetailsPage : ContentPage
{
	private readonly OrderDetailViewModel _orderDetailViewModel;
	public OrderDetailsPage(OrderDetailViewModel orderDetailViewModel)
	{
		InitializeComponent();
		BindingContext = _orderDetailViewModel = orderDetailViewModel;
	}
}