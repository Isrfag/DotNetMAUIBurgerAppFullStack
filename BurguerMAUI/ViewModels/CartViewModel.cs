using Burger.Shared.Dtos;
using BurguerMAUI.Data.Entities;
using BurguerMAUI.Models;
using BurguerMAUI.Pages;
using BurguerMAUI.Services;
using CommunityToolkit.Mvvm.Input;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.ViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        private readonly DataBaseService _dataBaseService;
        private readonly IOrderApi _orderApi;
        private readonly AuthService _authService;
        public CartViewModel(DataBaseService dataBaseService, IOrderApi orderApi, AuthService authService)
        {
            _dataBaseService = dataBaseService;
            _orderApi = orderApi;
            _authService = authService;
        }
        public ObservableCollection<CartItem> CartItems { get; set; } = [];

        public static int TotalCartCount { get; set; }
        public static event EventHandler<int>? TotalCartCountChanged;

        public async void AddItemToCart(BurgerDto burger, int quantity, string meat, string letuce, string bacon, string caramelizedOnion, string friedEgg, string regOnion, string tomato, string cheeseType, string sauce)
        {
            CartItem existingItem = CartItems.FirstOrDefault(item => item.BurgerId == burger.id);

            if (existingItem is not null)
            {
                CartItemEntity dbCartItem = await _dataBaseService.GetCartItemAysnc(existingItem.Id);

                if (quantity <= 0)
                {
                    //Usuario puede querer borrar este item del carrito

                    await _dataBaseService.DeleteCartItem(dbCartItem);

                    CartItems.Remove(existingItem);
                    await ShowToastAsync("Burger removed from cart");
                }
                else
                {
                    dbCartItem.Quantity = quantity;
                    await _dataBaseService.UpdateCartItem(dbCartItem);

                    existingItem.Quantity = quantity;
                    await ShowToastAsync("Cart updated");
                }
            }
            else
            {
                CartItem cartItem = new CartItem
                {
                    Id = burger.id,
                    BurgerId = burger.id,
                    Name = burger.Name,
                    Price = burger.Price,
                    Quantity = quantity,
                    Meat = meat,
                    Letuce=letuce,
                    Bacon = bacon,
                    CacaramelizedOnion = caramelizedOnion,
                    FriedEgg = friedEgg,
                    RegOnion = regOnion,
                    Tomato = tomato,
                    CheeseType = cheeseType,
                    Sauce = sauce
                };

                CartItemEntity entity = new Data.Entities.CartItemEntity(cartItem);

                await _dataBaseService.AddCartItem(entity);

                cartItem.Id = entity.Id;

                CartItems.Add(cartItem);

                await ShowToastAsync("Burger added to cart");
            }

            NotifyCartCountChange();
        }

        private void NotifyCartCountChange()
        {
            TotalCartCount = CartItems.Sum(item => item.Quantity);
            TotalCartCountChanged?.Invoke(null, TotalCartCount);
        }

        public int GetItemCartCount (int burgerId)
        {
            CartItem existingItems = CartItems.FirstOrDefault(item => item.BurgerId == burgerId);
            return existingItems?.Quantity ?? 0;
        }

        public async Task InitialzeCartAsync ()
        {
            List<CartItemEntity> dbItems = await _dataBaseService.GetAllCartItemEntityAsync();

            foreach (var dbItem in dbItems)
            {
                CartItems.Add(dbItem.ToCartItemModel());
            }
            NotifyCartCountChange ();

        }

        [RelayCommand]
        private async Task ClearCartAsync ()
        {
            await ClearCartInternalAsync(fromPlaceOrder: false);
        }

        private async Task ClearCartInternalAsync (bool fromPlaceOrder)
        {

            if( !fromPlaceOrder || CartItems.Count == 0)
            {
                await ShowAlertAsync("Empty Cart", "There aren't items in the cart");
                return;
            }

            if(fromPlaceOrder || await ConfirmAsync("Clear Cart?","Do you want to clear all the items from cart?"))
            {
                await _dataBaseService.ClearCartAsync();
                CartItems.Clear();

                if (!fromPlaceOrder)
                    await ShowToastAsync("Cart cleared");

                NotifyCartCountChange();
            }
        }

        [RelayCommand]
        private async Task ClearCartItemsAsync (int cartItemId)
        {
            if (await ConfirmAsync("Remove item from cart?","Do you want to remove this item from cart?"))
            {
                CartItem existingItem = CartItems.FirstOrDefault(i => i.Id == cartItemId);
                if (existingItem is null)
                    return;

                CartItems.Remove(existingItem);

                CartItemEntity dbCartItem = await _dataBaseService.GetCartItemAysnc(existingItem.Id);
                if (dbCartItem is null)
                    return;

                await _dataBaseService.DeleteCartItem(dbCartItem);

                await ShowToastAsync("Burger cleared from cart");
                NotifyCartCountChange();
            }
        }

        [RelayCommand]
        private async Task PlaceOrderAsync () 
        {
            if (CartItems.Count == 0)
            {
                await ShowAlertAsync("Empty cart", "Please add some items to cart.");
                return;
            }
            IsBusy = true;
            try
            {
                var order = new OrderDto(0, DateTime.Now, CartItems.Sum(i => i.TotalPrice));
                var orderItems = CartItems.Select(i => 
                                    new OrderItemDto(
                                        0,
                                        i.BurgerId,
                                        i.Name,
                                        i.Quantity, 
                                        i.Price,
                                        i.Meat,
                                        i.Letuce,
                                        i.Bacon,
                                        i.CacaramelizedOnion,
                                        i.FriedEgg,
                                        i.RegOnion,
                                        i.Tomato,
                                        i.CheeseType,
                                        i.Sauce
                                        )).ToArray();
                var orderPlaceDto = new OrderPlaceDto(order, orderItems);

                var result = await _orderApi.PlaceOrderAsync(orderPlaceDto);

                if (!result.IsSuccess)
                {
                    await ShowErrorAlertAsync(result.ErrorMessage!);
                    return;
                }
                // Si el resultado funciona
                // Orden enviada correctamente
                // Limpiar el carrito
                // Enseñar el mensaje
                await ShowToastAsync("Order placed");
                await ClearCartInternalAsync(fromPlaceOrder: true);
            }
            catch (ApiException e)
            {
                await HandleApiExceptionAsync(e, () => _authService.SignOut());
                await ShowAlertAsync(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
