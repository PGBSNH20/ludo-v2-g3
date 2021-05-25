using Backend.Model;
using System;

namespace Backend.GameLogics
{
    public class NewPawnPosition : INewPawnPosition
    {
        public NewPawnPosition()
        {
        }

        public void Calculate(int latestRoll, int enterFinishLine, IPawn pawn)
        {
            for (int i = 1; i <= latestRoll; i++)
            {
                pawn.Position++;

                if ((pawn.Position) == enterFinishLine && !pawn.AtFinishLine)
                {
                    pawn.Position = 0;
                    pawn.AtFinishLine = true;
                }

                if (pawn.Position > 44)
                {
                    pawn.Position = 1;
                }

                if (pawn.Position >= 4 && pawn.AtFinishLine)
                {
                    pawn.Position = 0;
                    pawn.AtFinishLine = false;
                    pawn.IsFinished = true;
                }
            }
        }
    }
}