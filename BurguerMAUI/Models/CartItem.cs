using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.Models
{
    public partial class CartItem : ObservableObject
    {
        public int Id { get; set; }
        public int BurgerId { get; set; }
        public string Name { get; set; }    
        public double Price { get; set; }
        public string Meat { get; set; }
        public string Letuce { get; set; }
        public string Bacon { get; set; }
        public string CacaramelizedOnion { get; set; }
        public string FriedEgg { get; set; }
        public string RegOnion { get; set; }
        public string Tomato { get; set; }
        public string CheeseType { get; set; }
        public string Sauce { get; set; }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(TotalPrice))]
        private int _quantity;

        public double TotalPrice => Price * Quantity;


    }
}
