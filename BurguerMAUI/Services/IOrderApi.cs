using Burger.Shared.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.Services
{
    [Headers("Authorization: Bearer")]
    public interface IOrderApi
    {
        [Post("/api/orders/place-order")]
        Task<ResultDto> PlaceOrderAsync(OrderPlaceDto dto);

        [Get("/api/orders")]
        Task<OrderDto[]> GetUserOrderASync();

        [Get("/api/orders/{orderId}/items")]
        Task<OrderItemDto[]> GetUserOrderItemsAsync(long orderId);
    }

}
