using BurguerMAUI.Pages;
using BurguerMAUI.Services;

namespace BurguerMAUI
{
    public partial class AppShell : Shell
    {
        private readonly AuthService _authService;
        public AppShell(AuthService authService)
        {
            InitializeComponent();
            RegisterRoutes();
            _authService = authService;

        }

        //Lo hacemos con un array para poder añadir mas facilmente
        private readonly static Type[] _routablePageType =
            [
                typeof(SignInPage),
                typeof(SignUpPage),
                typeof(MyOrdersPage),
                typeof(OrderDetailsPage),
                typeof(DetailsPage)
            ];

        private static void RegisterRoutes()
        {
            foreach (var pageType in _routablePageType)
            {
                Routing.RegisterRoute(pageType.Name, pageType);
            }
            
        }

        private async void FlyoutFooter_Tapped (object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://www.linkedin.com/in/israel-fernández-agudo-459273279/");
        }

        private async void SignOut_Click (object sender, EventArgs e) 
        {
            _authService.SignOut();
            await Shell.Current.GoToAsync($"//{nameof(OnBoardingPage)}");
        }
    }
}
