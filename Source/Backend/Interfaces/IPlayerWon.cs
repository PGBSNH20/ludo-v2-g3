using System.Collections.Generic;
using Backend.Model;

namespace Backend.GameLogics
{
    public interface IPlayerWon
    {
        bool Check(List<Pawn> pawns);
    }
}