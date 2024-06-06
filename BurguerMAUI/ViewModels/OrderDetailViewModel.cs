using Burger.Shared.Dtos;
using BurguerMAUI.Pages;
using BurguerMAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.ViewModels
{
    [QueryProperty (nameof(OrderId), nameof(OrderId))]
    public partial class OrderDetailViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly IOrderApi _orderApi;

        public OrderDetailViewModel (AuthService authService, IOrderApi orderApi)
        {
            _authService = authService;
            _orderApi = orderApi;
        }

        [ObservableProperty]
        private long _orderId;

        [ObservableProperty]
        private OrderItemDto[] _orderItems = [];

        [ObservableProperty]
        private string _title = "Order Items";


        partial void OnOrderIdChanged(long value)
        {
            Title = $"Order #{value}";
            LoadOrderItemsAsync(value);
        }

        private async Task LoadOrderItemsAsync(long orderId)
        {
            IsBusy = true;

            try
            {
                OrderItems = await _orderApi.GetUserOrderItemsAsync(orderId);
            }
            catch (ApiException ex)
            {
                await HandleApiExceptionAsync(ex, () => _authService.SignOut());
            }
            finally
            {
                IsBusy = false;
            }
        }

        
    }
}
