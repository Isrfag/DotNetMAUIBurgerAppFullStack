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
                Quantity = Quantity
            };
        
    }
}
