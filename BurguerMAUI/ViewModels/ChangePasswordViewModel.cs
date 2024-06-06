using Burger.Shared.Dtos;
using BurguerMAUI.Controls;
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
    public partial class ChangePasswordViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly IAuthApi _authApi;
        public ChangePasswordViewModel(AuthService authService, IAuthApi authApi)
        {
            _authService = authService;
            _authApi = authApi;
        }

        [ObservableProperty,NotifyPropertyChangedFor(nameof(CanChangePassword))]
        private string? _oldPassword;

        [ObservableProperty,NotifyPropertyChangedFor(nameof(CanChangePassword))]
        private string? _newPassword;

        [ObservableProperty,NotifyPropertyChangedFor(nameof(CanChangePassword))]
        private string? _confimNewPassword;

        public bool CanChangePassword =>    
                                       !string.IsNullOrWhiteSpace(OldPassword)
                                    && !string.IsNullOrWhiteSpace(NewPassword)
                                    && !string.IsNullOrWhiteSpace(ConfimNewPassword);
        [RelayCommand]
        private async Task ChangePasswordAsync ()
        {
            if(NewPassword != ConfimNewPassword)
            {
                await ShowErrorAlertAsync("Password does not match!");
                return;
            }

            IsBusy = true;

            try
            {
                var dto = new ChangePasswordDto(OldPassword!, NewPassword!);
                var result = await _authApi.ChangePasswordAsync(dto);

                if (!result.IsSuccess)
                {
                    await ShowErrorAlertAsync(result.ErrorMessage);
                    return;
                }

                await ShowAlertAsync("Success","Password changed successfully");
                //reseteamos los campos
                OldPassword = NewPassword = ConfimNewPassword = null;
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
