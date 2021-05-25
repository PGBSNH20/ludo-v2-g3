using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameLogic
{
    public class PawnStartPosition : IPawnStartPosition
    {
        public int Get(string color)
        {
            //Start positioner 
            // Röd: 27
            // Grön: 38
            // Gul: 5
            // Blå: 16

            int startPosition;

            _ = color switch
            {
                "Blue" => startPosition = 16,
                "Yellow" => startPosition = 5,
                "Red" => startPosition = 27,
                _ => startPosition = 38,
            };
            return startPosition;
        }
    }
}