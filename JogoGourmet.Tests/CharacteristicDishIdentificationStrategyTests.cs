using JogoGourmet.Application.Interfaces;
using JogoGourmet.Application.Models;
using JogoGourmet.UI.Strategies;
using Moq;
using System.Windows.Forms;

namespace JogoGourmet.Tests
{
    public class CharacteristicDishIdentificationStrategyTests
    {
        [Fact]
        public void IdentifyDish_ShouldReturnTrue_WhenUserConfirms()
        {
            // Arrange
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockMessageBoxService.Setup(m => m.ShowMessageBox(It.IsAny<string>(), MessageBoxButtons.YesNo))
                                 .Returns(DialogResult.Yes);

            var strategy = new CharacteristicDishIdentificationStrategy(mockMessageBoxService.Object);
            var dish = new Dish("Lasanha", "Massa");

            // Act
            bool result = strategy.IdentifyDish(dish);

            // Assert
            Assert.True(result, "The dish should be identified correctly when user confirms.");
        }

        [Fact]
        public void IdentifyDish_ShouldReturnFalse_WhenUserDenies()
        {
            // Arrange
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockMessageBoxService.Setup(m => m.ShowMessageBox(It.IsAny<string>(), MessageBoxButtons.YesNo))
                                 .Returns(DialogResult.No);

            var strategy = new CharacteristicDishIdentificationStrategy(mockMessageBoxService.Object);
            var dish = new Dish("Bolo de Chocolate", "Bolo");

            // Act
            bool result = strategy.IdentifyDish(dish);

            // Assert
            Assert.False(result, "The dish should not be identified when user denies.");
        }

    }
}
