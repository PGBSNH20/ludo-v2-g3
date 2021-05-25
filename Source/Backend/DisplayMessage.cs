using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend
{
    public class DisplayMessage : IDisplayMessage
    {
        public string MustRollToMove()
        {
            return "You must roll the dice in order to move a pawn.";
        }
    }
}