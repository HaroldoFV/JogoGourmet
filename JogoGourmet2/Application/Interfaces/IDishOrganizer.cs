using JogoGourmet.Application.Models;

namespace JogoGourmet.Application.Interfaces
{
    public interface IDishOrganizer
    {
        public IEnumerable<Dish> GetAllDishes();
        public void AddNewDish(string dishName, string characteristic, string lastDishName);
        public string GetLastDishName();
    }
}
