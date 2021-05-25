using Backend.Model;

namespace Backend.GameLogics
{
    public interface IKnockPawn
    {
        void ByPosition(int atPosition, IGameSession gameSession);
    }
}