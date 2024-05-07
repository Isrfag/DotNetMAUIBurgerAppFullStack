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

        [ObservableProperty, NotifyPropertyChangedFor(nameof(TotalPrice))]
        private int _quantity;

        public double TotalPrice => Price * Quantity;


    }
}
