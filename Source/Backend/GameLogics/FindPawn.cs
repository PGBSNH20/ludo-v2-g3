using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enum;
using Backend.Model;

namespace Backend.GameLogics
{
    public class FindPawn : IFindPawn
    {
        private readonly IPawn _pawn;

        public FindPawn(IPawn pawn)
        {
            _pawn = pawn;
        }
        public IPawn ById(int id, List<Player> playerList)
        {
            //IPawn pawn = Factory.CreateNewPawn();
            IPawn pawn = _pawn;
            foreach (var player in playerList)
            {
                pawn = player.Pawns.FirstOrDefault(p => p.ID == id);
                if (pawn != null)
                {
                    break;
                }
            }
            return pawn;
        }

        public List<Pawn> ByColor(PawnColor color, List<Player> players)
        {
            return players.SelectMany(p => p.Pawns).Where(p => p.Color == color).ToList();
        }
    }
}