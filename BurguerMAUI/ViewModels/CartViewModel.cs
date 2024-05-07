using Burger.Shared.Dtos;
using BurguerMAUI.Data.Entities;
using BurguerMAUI.Models;
using BurguerMAUI.Services;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.ViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        private readonly DataBaseService _dataBaseService;
        public CartViewModel(DataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }
        public ObservableCollection<CartItem> CartItems { get; set; } = [];

        public static int TotalCartCount { get; set; }
        public static event EventHandler<int>? TotalCartCountChanged;

        public async void AddItemToCart(BurgerDto burger, int quantity)
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
            if (CartItems.Count > 0) { 
                if (await ConfirmAsync("Clear Cart?","Do you really want to clear all the items from the cart?"))
                {
                    await _dataBaseService.ClearCartAsync(); //Borramos de la db
                    CartItems.Clear(); //Tb borramos la colecion

                    await ShowToastAsync("Cart cleared");

                    NotifyCartCountChange();
                }
            }else
            {
                await ShowAlertAsync("Cart empty", "There are no items in the cart");
                return;
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
    }
}
