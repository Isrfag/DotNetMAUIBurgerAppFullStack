using BurguerMAUI.Controls;
using BurguerMAUI.Pages;
using BurguerMAUI.Services;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.ViewModels
{
    public partial class ProfileViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly ChangePasswordViewModel _changePasswordViewModel;
        public ProfileViewModel(AuthService authService, ChangePasswordViewModel passwordViewModel) 
        {
            _authService=authService;
            _changePasswordViewModel=passwordViewModel;
        }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Initials))]
        private string _name = "";

        public string Initials
        {
            get
            {
                //Esto pasará el nombre por ejemplo Israel Fernandez -> parts[0] = Israel  parts[1] = Fernandez
                var parts = Name.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 1)
                {
                    return $"{parts[0][0]}{parts[1][0]}".ToUpper(); // Nos va a dar IF
                }
                return Name.Length > 1 ? Name[..1] : Name.ToUpper();
            }
        }

        public void Initialize()
        {
            Name = _authService.User!.Name;
        }

        [RelayCommand]
        private async Task SignOutAsync()
        {
            _authService.SignOut();
            await GoToAsync($"//{nameof(OnBoardingPage)}");
        }

        [RelayCommand]
        private async Task GoToMyOrdersAsync() => await GoToAsync(nameof(MyOrdersPage), animate: true);


        //Popup del cambio de contraseña
        [RelayCommand]
        private async Task ChangePasswordAsync()
        {
            await Shell.Current.CurrentPage.ShowPopupAsync(new ChangePasswordControl(_changePasswordViewModel));
        }
    }

    
}
