using Burger.Shared.Dtos;
using BurgerAPI.Api.Services;
using System.Security.Claims;

namespace BurgerAPI.Api.Controllers
{
    public static class UserController
    {
        private static Guid GetUserId(this ClaimsPrincipal principal) =>
            Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)!);
        public static IEndpointRouteBuilder ManageUsers (this IEndpointRouteBuilder app)
        {
            //enpoint para registrarse
            app.MapPost("api/auth/signup",
                async (SignUpRequestDto dto, AuthService authService) =>
                TypedResults.Ok(await authService.SignUpAsync(dto))
                );

            //enpoint para loguearse
            app.MapPost("api/auth/signin",
                async (SignInRequestDto dto, AuthService authService) =>
                    TypedResults.Ok(await authService.SignInAsync(dto))
            );
            //cambiar contraseña
            app.MapPut("/api/auth/change-password",
                async (ChangePasswordDto dto, ClaimsPrincipal principal, AuthService authService) =>
                    TypedResults.Ok(await authService.ChangePasswordAsync(dto, principal.GetUserId())))
                .RequireAuthorization();

            return app;

        }

    }
}
