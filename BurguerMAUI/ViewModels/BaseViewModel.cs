using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;

        protected async Task GoToAsync(string url, bool animate = false) =>
            await Shell.Current.GoToAsync(url,animate);

        protected async Task GoToAsync(string url, bool animate, IDictionary<string,object> parameters ) =>
            await Shell.Current.GoToAsync(url, animate, parameters);

        protected async Task ShowErrorAlertAsync(string errorMessage) =>
            await Shell.Current.DisplayAlert("Error", errorMessage, "Ok");

        protected async Task ShowAlertAsync(string message) =>
            await ShowAlertAsync("Alert", message);

        protected async Task ShowAlertAsync(string tittle,string message) =>
            await Shell.Current.DisplayAlert("Alert", message, "Ok");

        protected async Task ShowToastAsync(string message) =>
            await Toast.Make(message).Show();

        protected async Task<bool> ConfirmAsync(string title, string message) =>
            await Shell.Current.DisplayAlert(title, message, "Yes", "No");
        
    }
}
