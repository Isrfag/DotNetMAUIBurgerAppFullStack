using Burger.Shared.Dtos;
using BurgerAPI.Api.Services;

namespace BurgerAPI.Api.Controllers
{
    public static class UserController
    {
        public static IEndpointRouteBuilder SignUpAndSignInUsers (this IEndpointRouteBuilder app)
        {
            //enpoint para regustrarse
            app.MapPost("api/signup",
                async (SignUpRequestDto dto, AuthService authService) =>
                TypedResults.Ok(await authService.SignUpAsync(dto))
                );

            //enpoint para logearse
            app.MapPost("api/signin",
                async (SignInRequestDto dto, AuthService authService) =>
                    TypedResults.Ok(await authService.SignInAsync(dto))
            );
            return app;

        }

    }
}
