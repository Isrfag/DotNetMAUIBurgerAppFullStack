using Burger.Shared.Dtos;
using BurguerMAUI.Pages;
using BurguerMAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private IBurgerApi _burgerApi;
        private AuthService _authService;
        public HomeViewModel(IBurgerApi burgerApi, AuthService authService) 
        {
           _burgerApi = burgerApi;
           _authService = authService;
        }
        
        [ObservableProperty]
        private BurgerDto[] _burgers = [];

        [ObservableProperty]
        private string _userName = string.Empty;

        private bool _isInitizalized = false;

        public async Task InitializeAsync()
        {
            UserName = _authService.User!.Name;

            if (_isInitizalized) return;

            IsBusy = true;

            try 
            {
                //Llamada a la api
                Burgers = await _burgerApi.GetBurgersFromDb();
                _isInitizalized=true;

            }
            catch (Exception ex)
            {
                _isInitizalized=false;
                await ShowAlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

        }

        [RelayCommand]
        private async Task GoToDetailsPageAsync(BurgerDto burger)
        {
            var parameter = new Dictionary<string, object>
            {
                [nameof(DetailsViewModel.Burger)] = burger,
            };
            await GoToAsync(nameof(DetailsPage), animate: true, parameter);
        }
    }
}
