using Backend.Model;

namespace Backend.GameLogics
{
    public interface INewPawnPosition
    {
        void Calculate(int latestRoll, int enterFinishLine, IPawn pawn);
    }
}