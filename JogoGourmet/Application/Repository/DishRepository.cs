using JogoGourmet.Application.Models;

namespace JogoGourmet.Application.Repository
{
    public class DishRepository : IDishRepository
    {
        private readonly List<Dish> _dishes = new List<Dish>();

        public DishRepository()
        {
            InitializeDishes();
        }

        public void AddDish(Dish dish) => _dishes.Add(dish);
        public void RemoveDish(Dish dish) => _dishes.Remove(dish);
        public IEnumerable<Dish> GetAllDishes() => _dishes;
        public Dish FindDishByName(string name) => _dishes.FirstOrDefault(d => d.Name == name);

        private void InitializeDishes()
        {
            AddDish(new Dish("Lasanha", "Massa"));
            AddDish(new Dish("Bolo de Chocolate", "Bolo de Chocolate"));
        }
    }
}
