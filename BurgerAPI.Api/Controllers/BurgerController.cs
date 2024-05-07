using BurgerAPI.Api.Services;
using Burger.Shared.Dtos;

namespace BurgerAPI.Api.Controllers
{
    public static class BurgerController
    {
        public static IEndpointRouteBuilder GetBurgers ( this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/burgers",
                async(BurgerService burgerService) =>
                    TypedResults.Ok(await burgerService.GetBurgersFromDb()));

            return app;
                
        }
    }
}
