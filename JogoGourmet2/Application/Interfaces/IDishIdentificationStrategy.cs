using JogoGourmet.Application.Models;

namespace JogoGourmet.Application.Interfaces
{
    public interface IDishIdentificationStrategy
    {
        bool IdentifyDish(Dish dish);
    }
}
