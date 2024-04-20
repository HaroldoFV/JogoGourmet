using JogoGourmet.Application.Helpers;
using JogoGourmet.Application.Interfaces;
using JogoGourmet.Application.Repository;
using JogoGourmet.Application.Services;
using JogoGourmet.UI.Strategies;

namespace JogoGourmet
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IMessageBoxService messageBoxService = new MessageBoxService();
            IList<IDishIdentificationStrategy> dishIdentificationStrategies = new
                List<IDishIdentificationStrategy>()
            {
              new CharacteristicDishIdentificationStrategy(messageBoxService),
              new NameDishIdentificationStrategy(messageBoxService),
            };
            IDishRepository dishRepository = new DishRepository();
            IUserInteractionService userInteraction = new UserInteractionService(messageBoxService);
            IDishOrganizer dishOrganizer = new DishOrganizer(dishRepository);

            GamePlayService game = new GamePlayService(dishIdentificationStrategies, messageBoxService,
                                                       dishOrganizer, userInteraction);
            game.StartGame();
        }
    }
}