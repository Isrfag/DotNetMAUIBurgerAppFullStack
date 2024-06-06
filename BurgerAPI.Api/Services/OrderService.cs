using Burger.Shared.Dtos;
using BurgerAPI.Api.Data;
using BurgerAPI.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BurgerAPI.Api.Services
{
    public class OrderService
    {
        public DataContext _dataContext;

        public OrderService(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public async Task<ResultDto> PlaceOrderAsync (OrderPlaceDto dto, Guid customerId)
        {
            User customer = await _dataContext.Users.FirstOrDefaultAsync(user => user.Id == customerId);

            if (customer is null)
                return ResultDto.Failure("Customer does not exist");

            IEnumerable<OrderItem> orderItems = dto.Items.Select(i =>
                new OrderItem
                {
                    Meat = i.Meat,
                    Letuce = i.Letuce,
                    Bacon = i.Bacon,
                    CacaramelizedOnion = i.CaramelizedOnion,
                    FriedEgg = i.FriedEgg,
                    RegOnion = i.RegOnion,
                    Tomato = i.Tomato,
                    CheeseType = i.CheeseType,
                    Sauce = i.Sauce,
                    Name = i.Name,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    id = i.Id,
                    BurgerId = i.BurgerId,
                    TotalPrice= i.TotalPrice
                });

            Order order = new Order
            {
                CustomerId = customerId,
                CustomerAddress = customer.Address,
                CustomerEmail = customer.Email,
                CustomerName = customer.Name,
                OrderAt = DateTime.Now,
                TotalPrice = orderItems.Sum(o => o.TotalPrice),
                Items = orderItems.ToArray()
            };

            try 
            { 
                //GUARDAMOS EN LA DB
                await _dataContext.Orders.AddAsync(order);
                await _dataContext.SaveChangesAsync();
                return ResultDto.Success();
            }
            catch (Exception e)
            {
                return ResultDto.Failure(e.Message);
            }
        }

        public async Task<OrderDto[]> GetUserOrderAsync (Guid userId) =>        
            await _dataContext.Orders
                           .Where (order => order.CustomerId == userId)
                           .Select(order => new OrderDto (order.id, order.OrderAt, order.TotalPrice, order.Items.Sum(i => i.Quantity)))
                           .ToArrayAsync();
        
        public async Task<OrderItemDto[]> GetUserOrderItemsAsync (long orderId, Guid userId) =>
            await _dataContext.OrderItems
                                .Where(item => item.OrderId == orderId &&  item.Order.CustomerId == userId)
                                .Select(item => new OrderItemDto (
                                        item.id, 
                                        item.BurgerId, 
                                        item.Name, 
                                        item.Quantity,
                                        item.Price,
                                        item.Meat,
                                        item.Letuce,
                                        item.Bacon,
                                        item.CacaramelizedOnion,
                                        item.FriedEgg,
                                        item.RegOnion,
                                        item.Tomato,
                                        item.CheeseType,
                                        item.Sauce
                                        )
                                ).ToArrayAsync();

    }
}
