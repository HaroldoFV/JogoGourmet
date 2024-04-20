using JogoGourmet.Application.Interfaces;
using JogoGourmet.Application.Models;

namespace JogoGourmet.UI.Strategies
{
    public class NameDishIdentificationStrategy : IDishIdentificationStrategy
    {
        private readonly IMessageBoxService _messageBoxService;

        public NameDishIdentificationStrategy(IMessageBoxService messageBoxService)
        {
            _messageBoxService = messageBoxService;
        }

        public bool IdentifyDish(Dish dish)
        {
            var result = _messageBoxService.ShowMessageBox($"O prato que pensou é {dish.Name}?", MessageBoxButtons.YesNo);
            return result == DialogResult.Yes;
        }
    }

}
