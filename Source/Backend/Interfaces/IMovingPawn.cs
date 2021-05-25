using Backend.Controllers;

namespace Backend.GameLogics
{
    public interface IMovingPawn
    {
        string Move(MovePawnRequest request);
    }
}