using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend.GameLogics
{
    public class KnockPawn : IKnockPawn
    {
        public void ByPosition(int atPosition, IGameSession gameSession)
        {
            Pawn pawnToKnock = gameSession.Players.Select(x => x.Pawns.Find(p => p.Position == atPosition)).FirstOrDefault();
            if (pawnToKnock != null)
            {
                pawnToKnock.Position = 0;
                pawnToKnock.IsInNest = true;
            }
        }
    }
}