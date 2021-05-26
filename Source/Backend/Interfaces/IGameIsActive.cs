using System.Collections.Generic;
using Backend.Model;

namespace Backend.GameLogics
{
    public interface IGameIsActive
    {
        bool Check(List<Pawn> pawns);
    }
}