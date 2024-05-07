using System.ComponentModel.DataAnnotations;

namespace BurgerAPI.Api.Data.Entities
{
    public class BurgerOption
    {
        
        public int BurgerId { get; set; }

        [Required, MaxLength(50)]
        public string Meat { get; set; }

        [Required, MaxLength(10)]
        public string Letuce { get; set; }

        [Required, MaxLength(10)]
        public string Bacon { get; set; }

        [Required, MaxLength(10)]
        public string Tomato { get; set; }

        [Required, MaxLength(10)]
        public string FriedEgg { get; set; }

        [Required, MaxLength(10)]
        public string RegOnion { get; set; }

        [Required, MaxLength(10)]
        public string CacaramelizedOnion { get; set; }

        [Required, MaxLength(100)]
        public string Sauce { get; set; }

        [Required, MaxLength(120)]
        public string CheeseType { get; set; }

        public virtual TheBurger Burger { get; set;}
    }

}
