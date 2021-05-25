using System.Collections.Generic;
using Backend.Enum;
using Backend.Model;

namespace Backend.GameLogics
{
    public interface IFindPawn
    {
        IPawn ById(int id, List<Player> playerList);
        List<Pawn> ByColor(PawnColor color, List<Player> players);
    }
}