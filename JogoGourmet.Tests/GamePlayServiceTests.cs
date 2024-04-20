using JogoGourmet.Application.Interfaces;
using JogoGourmet.Application.Models;
using JogoGourmet.Application.Repository;
using JogoGourmet.Application.Services;
using Moq;
using System.Windows.Forms;

namespace JogoGourmet.Tests
{
    public class GamePlayServiceTests
    {
        private readonly Mock<IDishIdentificationStrategy> _mockCharacteristicStrategy;
        private readonly Mock<IDishIdentificationStrategy> _mockNameStrategy;
        private readonly Mock<IMessageBoxService> _mockMessageBoxService;
        private readonly Mock<IDishRepository> _mockDishRepository;
        private readonly Mock<IDishOrganizer> _mockDishOrganizer;
        private readonly Mock<IUserInteractionService> _mockUserInteractionService;
        private readonly GamePlayService _gamePlayService;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public GamePlayServiceTests()
        {
            _mockCharacteristicStrategy = new Mock<IDishIdentificationStrategy>();
            _mockNameStrategy = new Mock<IDishIdentificationStrategy>();
            _mockMessageBoxService = new Mock<IMessageBoxService>();
            _mockDishRepository = new Mock<IDishRepository>();
            _mockDishOrganizer = new Mock<IDishOrganizer>();
            _mockUserInteractionService = new Mock<IUserInteractionService>();

            _gamePlayService = new GamePlayService(
                new List<IDishIdentificationStrategy> { _mockCharacteristicStrategy.Object, _mockNameStrategy.Object },
                _mockMessageBoxService.Object,
                _mockDishOrganizer.Object,
                _mockUserInteractionService.Object
            );

            _cancellationTokenSource = new CancellationTokenSource();

        }

        [Fact]
        public void GetDishes_ReturnsAllDishesFromRepository()
        {
            // Arrange
            var expectedDishes = new List<Dish> { new Dish("Pizza", "Italian"), new Dish("Sushi", "Japanese") };
            _mockDishOrganizer.Setup(organizer => organizer.GetAllDishes()).Returns(expectedDishes); // Configurando o mock do DishOrganizer

            // Act
            var dishes = _gamePlayService.GetDishes();

            // Assert
            Assert.Equal(expectedDishes, dishes);
        }

        [Fact]
        public void StartGame_DisplaysInitialMessage()
        {
            // Arrange
            var cts = new CancellationTokenSource();
            cts.CancelAfter(1000); // Cancela após 1 segundo para evitar o loop infinito durante o teste

            _mockMessageBoxService.Setup(m => m.ShowMessageBox(It.IsAny<string>(), MessageBoxButtons.OK))
                .Returns(DialogResult.OK);

            // Act
            var ex = Record.Exception(() => _gamePlayService.StartGame(cts.Token));

            // Assert
            Assert.Null(ex);
            _mockMessageBoxService.Verify(m => m.ShowMessageBox("Pense em um prato que gosta.", MessageBoxButtons.OK), Times.AtLeastOnce());
        }
    }

}

