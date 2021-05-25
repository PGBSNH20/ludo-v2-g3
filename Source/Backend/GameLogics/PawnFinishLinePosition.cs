using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameLogic
{
    public class PawnFinishLinePosition : IPawnFinishLinePosition
    {
        public int Get(string color)
        {
            int finishLinePosition;

            _ = color switch
            {
                "Blue" => finishLinePosition = 16,
                "Yellow" => finishLinePosition = 5,
                "Red" => finishLinePosition = 27,
                _ => finishLinePosition = 38,
            };
            return finishLinePosition;
        }
    }
}