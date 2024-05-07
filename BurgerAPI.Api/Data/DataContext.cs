using Microsoft.EntityFrameworkCore;
using BurgerAPI.Api.Data.Entities;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace BurgerAPI.Api.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions <DataContext> options) : base (options) // Constructor Que instancia las options (ID)
        {
            
        }

        public DbSet <User> Users { get; set; }
        public DbSet <TheBurger> Burgers { get; set; }
        public DbSet <BurgerOption> BurgerOptions { get; set; }
        public DbSet <Order> Orders { get; set; }
        public DbSet <OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            modelBuilder.Entity<BurgerOption>()
                .HasKey(io => new { io.BurgerId, io.Meat,io.Letuce, io.Bacon, io.CacaramelizedOnion, io.FriedEgg, io.RegOnion, io.Tomato, io.CheeseType, io.Sauce });

            AddSeedData(modelBuilder);

        }

        private static void AddSeedData (ModelBuilder modelBuilder)
        {
            TheBurger[] burgers = [

                new TheBurger { Id=1, Name="Delicious Mexican", Image= "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg1.jpg" , price = 10.98 },
                new TheBurger { Id=2, Name="Classic Burger", Image= "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg2.jpg" , price =8.99 },
                new TheBurger { Id=3, Name="Capra", Image= "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg3.jpg" , price = 10.99},
                new TheBurger { Id=4, Name="Cheese Madness", Image= "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg4.jpg" , price = 11.50 },
                new TheBurger { Id=5, Name="Minimalist", Image= "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg5.jpg" , price = 7.85 },
                new TheBurger { Id=6, Name="Chicken Delight", Image= "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg6.jpg" , price = 8.95},
                new TheBurger { Id=7, Name="San Francisco", Image= "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg7.jpg" , price = 10.80 },
                new TheBurger { Id=8, Name="Full Meat Assault", Image= "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg8.png" , price = 13.25 },
                new TheBurger { Id=9, Name="Carbonara", Image= "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg9.jpg" , price = 12.99  },
                new TheBurger { Id=10, Name="No Survival", Image= "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg10.jpg" , price = 19.99 }
           ];

            BurgerOption[] burgerOptions = [

                new BurgerOption { BurgerId =1 , Meat = "beef", Letuce="no",Bacon="yes", CacaramelizedOnion="no",FriedEgg="no",
                                         RegOnion="no",Tomato="no",CheeseType="none", Sauce="guacamole"},

                new BurgerOption { BurgerId =2 ,  Meat = "beef",Letuce="yes",Bacon="no", CacaramelizedOnion="no",FriedEgg="yes",
                                         RegOnion="yes",Tomato="yes",CheeseType="cheddar", Sauce="none"},

                new BurgerOption { BurgerId =3 , Meat ="beef",Letuce="no",Bacon="no", CacaramelizedOnion="yes",FriedEgg="no",
                                         RegOnion="no",Tomato="no",CheeseType="goat cheese", Sauce="none"},

                new BurgerOption { BurgerId =4 ,  Meat = "Double Beef",Letuce="no",Bacon="no", CacaramelizedOnion="no",FriedEgg="no",
                                         RegOnion="no",Tomato="no", CheeseType="cheddar,camembert,roquefort,mozzarella", Sauce="cheddar sauce"},

                new BurgerOption { BurgerId =5 , Meat = "beef",Letuce="no",Bacon="no", CacaramelizedOnion="no",FriedEgg="no",
                                         RegOnion="no",Tomato="no",CheeseType="cheddar", Sauce="none"},

                new BurgerOption { BurgerId =6 ,  Meat = "chicken",Letuce="yes",Bacon="no", CacaramelizedOnion="no",FriedEgg="no",
                                         RegOnion="no",Tomato="yes",CheeseType="cheddar", Sauce="mayonnaise"},

                new BurgerOption { BurgerId =7 ,  Meat = "beef",Letuce="no",Bacon="yes", CacaramelizedOnion="no",FriedEgg="yes",
                                         RegOnion="no", Tomato="no",CheeseType="double cheddar", Sauce="mayonnaise"},

                new BurgerOption { BurgerId =8 ,  Meat = "double beef",Letuce="no", Bacon="yes", CacaramelizedOnion="no",FriedEgg="no",
                                         RegOnion="no",Tomato="no",CheeseType="cheddar", Sauce="none"},

                new BurgerOption { BurgerId =9 ,  Meat = "beef",Letuce="no",Bacon="yes", CacaramelizedOnion="no",FriedEgg="yes",
                                         RegOnion="yes",Tomato="no",CheeseType="parmesan", Sauce="cream"},

                new BurgerOption { BurgerId =10 , Meat = "quadruple beef",Letuce="yes" ,Bacon="yes",CacaramelizedOnion="no",FriedEgg="no",
                                         RegOnion="no",Tomato="yes",CheeseType="quadruple cheddar", Sauce="cheese"},
           ];


            modelBuilder.Entity<TheBurger>().HasData(burgers);

            modelBuilder.Entity<BurgerOption>().HasData(burgerOptions);

        }
    }
}
