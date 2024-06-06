using Burger.Shared.Dtos;
using BurgerAPI.Api.Services;
using System.Security.Claims;

namespace BurgerAPI.Api.Controllers
{
    public static class OrderController
    {
        private static Guid GetUserId(this ClaimsPrincipal principal) =>
            Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public static IEndpointRouteBuilder saveOrders (this IEndpointRouteBuilder app)
        {
            var orderGroup = app.MapGroup("/api/orders").RequireAuthorization();

            orderGroup.MapPost("/place-order",
                async (OrderPlaceDto dto, ClaimsPrincipal principal, OrderService service) =>
                    await service.PlaceOrderAsync(dto, principal.GetUserId()));

            orderGroup.MapGet("",
                async (ClaimsPrincipal principal, OrderService orderService) =>
                        TypedResults.Ok(await orderService.GetUserOrderAsync(principal.GetUserId())));

            orderGroup.MapGet("/{orderId:long}/items",
                async (long orderId, ClaimsPrincipal principal, OrderService orderService) =>
                        TypedResults.Ok(await orderService.GetUserOrderItemsAsync(orderId,principal.GetUserId())));
            
            return app;
        }
    }
}
