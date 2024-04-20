using JogoGourmet.Application.Interfaces;
using JogoGourmet.Application.Models;
using JogoGourmet.Application.Repository;

namespace JogoGourmet.Application.Services
{
    public class DishOrganizer : IDishOrganizer
    {
        private readonly IDishRepository _dishRepository;

        public DishOrganizer(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public IEnumerable<Dish> GetAllDishes()
        {
            return _dishRepository.GetAllDishes();
        }

        public void AddNewDish(string dishName, string characteristic, string lastDishName)
        {
            if (dishName != lastDishName)
            {
                Dish newDish = new Dish(dishName, characteristic);
                _dishRepository.AddDish(newDish);
            }
            ReorganizeDish(lastDishName);
        }

        public string GetLastDishName()
        {
            var lastDish = _dishRepository.GetAllDishes().LastOrDefault();
            return lastDish?.Name;
        }

        private void ReorganizeDish(string lastDishName)
        {
            var lastDish = _dishRepository.FindDishByName(lastDishName);
            if (lastDish != null)
            {
                _dishRepository.RemoveDish(lastDish);
                _dishRepository.AddDish(lastDish);
            }
        }

    }
}
