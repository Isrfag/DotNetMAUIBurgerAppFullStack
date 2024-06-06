using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burger.Shared.Dtos
{
   
    public record OrderItemDto (long Id, int BurgerId, string Name, int Quantity, double Price, string Meat, string Letuce, string Bacon, string CaramelizedOnion, string FriedEgg, string RegOnion, string Tomato, string CheeseType, string Sauce)
    {
        public double TotalPrice => Quantity * Price;
    }

    public record OrderDto(long id, DateTime OrderAt, double TotalPrice, int TotalCount = 0)
    {
        public string BurgerCount => TotalCount + (TotalCount > 1 ? " Burgers" : " Burger");
    }

    public record OrderPlaceDto (OrderDto Order, OrderItemDto[] Items );
    
    
}
