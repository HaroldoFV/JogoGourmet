using JogoGourmet.Application.Helpers;
using JogoGourmet.Application.Interfaces;
using JogoGourmet.Application.Repository;
using JogoGourmet.Application.Services;
using JogoGourmet.UI.Strategies;

namespace JogoGourmet2
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form_Load);
            this.FormClosing += Form1_FormClosing;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancellationTokenSource.Cancel();
            MessageBox.Show("Fechando o formulário e encerrando a aplicação.");
            Application.Exit();
        }

        private void StartGame()
        {
            IMessageBoxService messageBoxService = new MessageBoxService();
            IList<IDishIdentificationStrategy> dishIdentificationStrategies = new List<IDishIdentificationStrategy>()
        {
            new CharacteristicDishIdentificationStrategy(messageBoxService),
            new NameDishIdentificationStrategy(messageBoxService),
        };
            IDishRepository dishRepository = new DishRepository();
            IUserInteractionService userInteraction = new UserInteractionService(messageBoxService);
            IDishOrganizer dishOrganizer = new DishOrganizer(dishRepository);

            GamePlayService game = new GamePlayService(dishIdentificationStrategies, messageBoxService, dishOrganizer, userInteraction);
            game.StartGame(cancellationTokenSource.Token);
        }
    }
}
