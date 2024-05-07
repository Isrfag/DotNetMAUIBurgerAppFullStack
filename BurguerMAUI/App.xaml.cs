using BurguerMAUI.Services;
using BurguerMAUI.ViewModels;

namespace BurguerMAUI
{
    public partial class App : Application
    {
        private readonly CartViewModel _cartViewModel;
        public App(AuthService authService, CartViewModel cartViewModel)
        {
            InitializeComponent();

            authService.Initialize();

            MainPage = new AppShell(authService);
            _cartViewModel = cartViewModel;
        }

        protected override async void OnStart()
        {
            await _cartViewModel.InitialzeCartAsync();
        }
    }
}
