using Burger.Shared.Dtos;
using BurguerMAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BurguerMAUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.ViewModels
{
    public partial class AuthViewModel : BaseViewModel
    {
        private readonly IAuthApi _authApi;

        private readonly AuthService _authService;

        public AuthViewModel(IAuthApi authApi, AuthService authService)
        {
            _authApi = authApi;
            _authService = authService;
        }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignUp))]
        private string? _name;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignIn)), NotifyPropertyChangedFor(nameof(CanSignUp))]
        private string? _email;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignIn)), NotifyPropertyChangedFor(nameof(CanSignUp))]
        private string? _password;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignUp))]
        private string? _address;

        public bool CanSignIn => !string.IsNullOrEmpty(Email) &&
                                 !string.IsNullOrEmpty(Password);
        public bool CanSignUp => CanSignIn &&
                                 !string.IsNullOrEmpty(Password) &&
                                 !string.IsNullOrEmpty(Address);
        
        [RelayCommand]
        private async Task SignUpAsync()
        {
            IsBusy = true;

            try
            {
                SignUpRequestDto signUpDto = new SignUpRequestDto(Name,Email,Password,Address);

                //LLAMADA A LA API
                ResultWithDataDto<AuthResponseDto> result = await _authApi.SignUpAsync(signUpDto);

                if(result.IsSuccess)
                {
                    _authService.SignIn(result.Data);
              
                    //A la pagina de inicio
                    await GoToAsync($"//{nameof(HomePage)}", animate: true);
                }
                else
                {
                    //Lanza error
                    await ShowErrorAlertAsync(result.errorMessage ?? "Unknown error");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorAlertAsync(ex.Message);
            }
            finally 
            { 
                IsBusy = false; 
            }
        }

        [RelayCommand]
        private async Task SignInAsync ()
        {
            IsBusy = true;

            try
            {
                SignInRequestDto signInDto = new SignInRequestDto(Email, Password);

                //Llamamos a la api
                ResultWithDataDto<AuthResponseDto> result = await _authApi.SignInAsync(signInDto);

                if (result.IsSuccess)
                {
                    _authService.SignIn(result.Data);
                    //Pagina de inicio
                    await GoToAsync($"//{nameof(HomePage)}", animate: true);
                }
                else
                {
                    await ShowErrorAlertAsync(result.errorMessage ?? "Unknown error when trying to sign in");
                }

            }
            catch (Exception ex)
            {
                await ShowErrorAlertAsync(ex.Message);
            }
            finally 
            {
                IsBusy = false;
            }

        }
    }
}
