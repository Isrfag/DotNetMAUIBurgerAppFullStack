using Burger.Shared.Dtos;
using Refit;

namespace BurguerMAUI.Services
{
    public interface IBurgerApi
    {
        [Get("/api/burgers")]
        Task<BurgerDto[]> GetBurgersFromDb();
    }
}
