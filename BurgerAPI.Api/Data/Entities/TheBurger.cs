using System.ComponentModel.DataAnnotations;

namespace BurgerAPI.Api.Data.Entities
{
    public class TheBurger
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Range(0.1,double.MaxValue)]
        public double price { get; set; }

        [Required, MaxLength(180)]
        public string Image { get; set; }

        public ICollection<BurgerOption> Options { get; set; }
    }

}
