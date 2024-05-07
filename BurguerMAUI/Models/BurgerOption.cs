using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.Models
{
    public partial class BurgerOption : ObservableObject
    {
        public string Meat { get; set; }
        public string Cheese { get; set; }
        public string Bacon { get; set; }
        public string Letuce { get; set; }
        public string Egg { get; set; }
        public string CaramelizedOnion { get; set; }
        public string RegularOnion { get; set; }
        public string Tomato { get; set; }
        public string Sauce { get; set; }

        [ObservableProperty]
        private bool _isSelected;

    }
}
