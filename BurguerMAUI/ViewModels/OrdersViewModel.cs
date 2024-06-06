using Burger.Shared.Dtos;
using BurguerMAUI.Pages;
using BurguerMAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.ViewModels
{
    public partial class OrdersViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly IOrderApi _orderApi;
        public OrdersViewModel(AuthService authService, IOrderApi orderApi) 
        {
            _authService = authService;
            _orderApi = orderApi;
        }

        [ObservableProperty]
        private OrderDto[] _orders = [];

        public async Task InitializeAsync () => await LoadOrdersAsync();

        [RelayCommand]
        public async Task LoadOrdersAsync ()
        {
            IsBusy = true;

            try
            {
                Orders = await _orderApi.GetUserOrderASync();
                if (Orders.Length == 0)
                {
                    await ShowToastAsync("No orders found");
                }

            }
            catch (ApiException ex)
            {
                await HandleApiExceptionAsync(ex, () => _authService.SignOut());

                await ShowErrorAlertAsync(ex.Message);
            }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        private async Task GoToOrderDetailsPageAsync(long orderId)
        {
            var parameter = new Dictionary<string, object>
            {
                [nameof(OrderDetailViewModel.OrderId)] = orderId,
            };

            await GoToAsync(nameof(OrderDetailsPage), animate: true, parameter);
        }
    }
}
