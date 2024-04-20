using JogoGourmet.Application.Interfaces;
using JogoGourmet.Application.Repository;
using JogoGourmet.UI.Strategies;
using System.Collections;

namespace JogoGourmet.Application.Services
{
    public class GamePlayService : IGamePlayService
    {
        private readonly IList<IDishIdentificationStrategy> _dishIdentificationStrategies;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IDishRepository _dishRepository;
        private readonly IDishOrganizer _dishOrganizer;
        private readonly IUserInteractionService _userInteraction;

        public GamePlayService(
            IList<IDishIdentificationStrategy> dishIdentificationStrategies,
            IMessageBoxService messageBoxService,
            IDishOrganizer dishOrganizer,
            IUserInteractionService userInteraction)
        {
            _dishIdentificationStrategies = dishIdentificationStrategies;
            _messageBoxService = messageBoxService;
            _userInteraction = new UserInteractionService(messageBoxService);
            _dishOrganizer = dishOrganizer;
            _userInteraction = userInteraction;
        }

        public IEnumerable GetDishes()
        {
            return _dishRepository.GetAllDishes();
        }

        public void StartGame(CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _messageBoxService.ShowMessageBox("Pense em um prato que gosta.", MessageBoxButtons.OK);
                bool restart = false;

                foreach (var dish in _dishOrganizer.GetAllDishes())
                {
                    if (cancellationToken.IsCancellationRequested) break;

                    bool result = false;
                    var characteristicStrategy = _dishIdentificationStrategies
                        .FirstOrDefault(s => s is CharacteristicDishIdentificationStrategy);

                    if (characteristicStrategy != null)
                    {
                        result = characteristicStrategy.IdentifyDish(dish);

                        if (result)
                        {
                            if (dish.Name == dish.Characteristic)
                            {
                                //_messageBoxService.ShowMessageBox("Acertei de novo!", MessageBoxButtons.OK);
                                DialogResult msgResult = _messageBoxService.ShowMessageBox("Acertei de novo! Deseja encerrar o jogo?", MessageBoxButtons.YesNo);
                                if (msgResult == DialogResult.Yes)
                                {
                                    Environment.Exit(0);
                                }
                                restart = true;
                                break;
                            }

                            var nameStrategy = _dishIdentificationStrategies.FirstOrDefault(s => s is NameDishIdentificationStrategy);
                            if (nameStrategy != null)
                            {
                                result = nameStrategy.IdentifyDish(dish);
                                if (result)
                                {
                                    DialogResult msgResult = _messageBoxService.ShowMessageBox("Acertei de novo!", MessageBoxButtons.OK);
                                    if (msgResult == DialogResult.Yes)
                                    {
                                        Environment.Exit(0);
                                    }
                                    restart = true;
                                    break;
                                }
                                else
                                {
                                    NewDish();
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                if (restart)
                {
                    continue;
                }

                NewDish();
                if (cancellationToken.IsCancellationRequested) break;
            }
        }


        private void NewDish()
        {
            string newDish = _userInteraction.PromptUser("Qual prato você pensou?");
            string lastDishName = _dishOrganizer.GetLastDishName();

            if (newDish != lastDishName)
            {
                string newCharacteristic = _userInteraction.PromptUser($"{newDish} é _________, mas {lastDishName} não.");
                _dishOrganizer.AddNewDish(newDish, newCharacteristic, lastDishName);
            }
        }
    }
}


