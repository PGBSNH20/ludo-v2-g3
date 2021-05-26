using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend.GameLogics
{
    public class PlayerWon : IPlayerWon
    {
        public bool Check(List<Pawn> pawns)
        {
            int finishedPawnCount = 0;
            foreach (var pawn in pawns)
            {
                if (pawn.IsFinished)
                {
                    finishedPawnCount++;
                }
            }
            return finishedPawnCount != 4;
        }
    }
}
