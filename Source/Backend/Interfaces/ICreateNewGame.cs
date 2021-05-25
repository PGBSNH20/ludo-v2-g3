using Backend.Model;
using Backend.Requests;

namespace Backend
{
    public interface ICreateNewGame
    {
        IGameSession Create(NewGameRequest request);
    }
}