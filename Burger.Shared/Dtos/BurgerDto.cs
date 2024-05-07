using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burger.Shared.Dtos
{
    public record struct BurgerOptionDto (string Meat, string Letuce, string Bacon, string CaramelizedOnion,string friedEgg, string RegOnion, string tomato,string CheeseType, string Sauce);
    public record BurgerDto(int id, string Name, string Image, double Price, BurgerOptionDto[] BurgerOptions);
}
