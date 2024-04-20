using System.Collections;

namespace JogoGourmet.Application.Interfaces
{
    public interface IGamePlayService
    {
        IEnumerable GetDishes();
        void StartGame(CancellationToken cancellationToken);
    }
}