using Burger.Shared.Dtos;
using BurgerAPI.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace BurgerAPI.Api.Services
{
    public class BurgerService
    {
        private readonly DataContext _dataContext;
        public BurgerService(DataContext context) 
        {
            _dataContext = context;
        }

        public async Task<BurgerDto[]> GetBurgersFromDb() => await _dataContext.Burgers.AsNoTracking()
           .Select(i =>
                new BurgerDto(
                    i.Id,
                    i.Name,
                    i.Image,
                    i.price,
                    i.Options.Select(
                        option => new BurgerOptionDto(
                                               option.Meat,
                                               option.Letuce,
                                               option.Bacon,
                                               option.CacaramelizedOnion,
                                               option.FriedEgg,
                                               option.RegOnion,
                                               option.Tomato,
                                               option.CheeseType,
                                               option.Sauce)
                        ).ToArray()
                    )).ToArrayAsync();
          
                                       
        
    }
}
