using JogoGourmet.Application.Helpers;
using JogoGourmet.Application.Interfaces;
using JogoGourmet.Application.Repository;
using JogoGourmet.Application.Services;
using JogoGourmet.UI.Strategies;

namespace JogoGourmet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
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
