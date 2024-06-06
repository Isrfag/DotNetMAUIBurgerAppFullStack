using BurguerMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.Data.Entities
{
    public class CartItemEntity
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int BurgerId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Meat { get; set; }
        public string Letuce { get; set; }
        public string Bacon { get; set; }
        public string CaramelizedOnion { get; set; }
        public string FriedEgg { get; set; }
        public string RegOnion { get; set; }
        public string Tomato { get; set; }
        public string CheeseType { get; set; }
        public string Sauce { get; set; }

        //Contructor con parametros
        public CartItemEntity(CartItem cartItemModel)
        {
            BurgerId = cartItemModel.BurgerId;
            Name = cartItemModel.Name;
            Price = cartItemModel.Price;
            Quantity = cartItemModel.Quantity;
            
        }

        //Contructor sin parametros
        public CartItemEntity()
        {
        }

        public CartItem ToCartItemModel() =>
            new CartItem
            {
                Id = Id,
                Name = Name,
                BurgerId = BurgerId,
                Price = Price,
                Quantity = Quantity,
                Meat = Meat,
                Letuce = Letuce,
                Bacon= Bacon,
                CacaramelizedOnion = CaramelizedOnion,
                FriedEgg = FriedEgg,
                RegOnion = RegOnion,
                Tomato = Tomato,
                CheeseType = CheeseType,
                Sauce = Sauce,
            };
        
    }
}
