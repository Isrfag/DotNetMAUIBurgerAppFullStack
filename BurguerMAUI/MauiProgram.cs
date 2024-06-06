using BurguerMAUI.Pages;
#if ANDROID
using BurguerMAUI.Platforms.Android.Resources;
#elif IOS
using BurguerMAUI.Platforms.iOS;
#endif
using BurguerMAUI.Services;
using BurguerMAUI.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Refit;

#if ANDROID
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Android.Net;
#elif IOS
using Security;
#endif

namespace BurguerMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit()
                .ConfigureMauiHandlers(handler =>
                {
#if ANDROID 
                    handler.AddHandler<Shell, TabBarBadgeRenderer>();
#elif IOS
                    handler.AddHandler<Shell, TabBarBadgeRenderer>();
#endif
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<AuthViewModel>()
                            .AddTransient<SignUpPage>()
                            .AddTransient<SignInPage>();

            builder.Services.AddSingleton<AuthService>();

            builder.Services.AddTransient<OnBoardingPage>();

            builder.Services.AddSingleton<HomeViewModel>()
                            .AddSingleton<HomePage>();

            builder.Services.AddTransient<DetailsViewModel>()
                            .AddTransient<DetailsPage>();

            builder.Services.AddSingleton<CartViewModel>()
                            .AddTransient<CartPage>();

            builder.Services.AddSingleton<DataBaseService>();

            builder.Services.AddTransient<ProfileViewModel>()
                            .AddTransient<ProfilePage>();

            builder.Services.AddTransient<OrdersViewModel>()
                            .AddTransient<MyOrdersPage>();

            builder.Services.AddTransient<OrderDetailViewModel>()
                            .AddTransient<OrderDetailsPage>();

            builder.Services.AddTransient<ChangePasswordViewModel>();

            ConfigureRefit(builder.Services);
            return builder.Build();
        }

        private static void ConfigureRefit(IServiceCollection services)
        {
            services.AddRefitClient<IAuthApi>(GetRefitSettings).ConfigureHttpClient(SetHttpClient);
        
            services.AddRefitClient<IBurgerApi>(GetRefitSettings).ConfigureHttpClient(SetHttpClient);

            services.AddRefitClient<IOrderApi>(GetRefitSettings).ConfigureHttpClient(SetHttpClient);

            static void SetHttpClient (HttpClient httpClient)
            {
                string baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                                            ? "https://10.0.2.2:7046"
                                            : "https://localhost:7046";
                
                if(DeviceInfo.DeviceType == DeviceType.Physical)
                {
                    baseUrl = "https://dgt8vprq-7046.uks1.devtunnels.ms";
                }

                httpClient.BaseAddress = new Uri(baseUrl);
            }

            static RefitSettings GetRefitSettings(IServiceProvider serviceProvider)
            {
                AuthService authService = serviceProvider.GetRequiredService<AuthService>();

                RefitSettings refitSettings = new()
                {
                    HttpMessageHandlerFactory = () =>
                    {
#if ANDROID
                    return new AndroidMessageHandler 
                    {
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate , chain , sslPolicyErrors) =>
                        {
                            return certificate?.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None;
                        }
                    };
#elif IOS
                        return new NSUrlSessionHandler
                        {
                            TrustOverrideForUrl = (NSUrlSessionHandler sender, string url, SecTrust trust) =>
                                url.StartsWith("https://localhost")
                        };
#endif
                        return null;
                    },
                    AuthorizationHeaderValueGetter = (_, __) => Task.FromResult(authService.Token ?? string.Empty)
                };
                return refitSettings;
            }

        }
    }
}
