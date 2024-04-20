using JogoGourmet.Application.Models;

namespace JogoGourmet.Application.Repository
{
    public interface IDishRepository
    {
        void AddDish(Dish dish);
        void RemoveDish(Dish dish);
        IEnumerable<Dish> GetAllDishes();
        Dish FindDishByName(string name);
    }
}
