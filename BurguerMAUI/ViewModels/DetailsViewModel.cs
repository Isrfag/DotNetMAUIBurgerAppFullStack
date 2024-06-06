using Burger.Shared.Dtos;
using BurguerMAUI.Models;
using BurguerMAUI.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.ViewModels
{
    [QueryProperty(nameof(Burger), nameof(Burger))]

    public partial class DetailsViewModel : BaseViewModel
    {
        private CartViewModel _cartViewModel;
        public DetailsViewModel(CartViewModel cartViewModel) 
        {
            _cartViewModel = cartViewModel;
        }
        [ObservableProperty]
        private BurgerDto? _burger;

        [ObservableProperty]
        private int _quantity;

        [ObservableProperty]
        private BurgerOption[] _options = [];

        partial void OnBurgerChanged(BurgerDto? value)
        {
            //Recuperamos el burgerDto y lo igualamos a nuestro Model que es un array.
            Options = [];
            if (value is null)
                return;

            Options = value.BurgerOptions.Select(option => new BurgerOption
            {
                Meat = option.Meat,
                Cheese = option.CheeseType,
                Bacon = option.Bacon,
                Letuce = option.Letuce,
                Egg = option.friedEgg,
                CaramelizedOnion = option.CaramelizedOnion,
                RegularOnion = option.RegOnion,
                Tomato = option.tomato,
                Sauce = option.Sauce,
                IsSelected=false

            }).ToArray();

            Quantity = _cartViewModel.GetItemCartCount(value.id);
        }

        [RelayCommand]
        private void IncreaseQuantity() => Quantity++;

        [RelayCommand]
        private void DecreaseQuantity() 
        { 
            if (Quantity <= 0) 
            {
                Quantity = 0;
            } 
            else { 
                Quantity--; 
            } 
        }

        [RelayCommand]
        private async Task GoBackAsync() => await GoToAsync("..",animate:true);

        [RelayCommand]
        private async Task AddToCart()
        {
            var selectedBurger = Options.FirstOrDefault(option => option.IsSelected) ?? Options[0];
            _cartViewModel.AddItemToCart(
                Burger,
                Quantity,
                selectedBurger.Meat,
                selectedBurger.Letuce,
                selectedBurger.Bacon,
                selectedBurger.CaramelizedOnion,
                selectedBurger.Egg,
                selectedBurger.RegularOnion,
                selectedBurger.Tomato,
                selectedBurger.Cheese,
                selectedBurger.Sauce);

            //Cerramos la pestaña al añadir al carrito
            await GoBackAsync();
        }

       
    }
}
