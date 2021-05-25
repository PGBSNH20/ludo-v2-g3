using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public class Pawn
    {
        public int ID { get; set; }
        public int Position { get; set; }
        public bool AtFinishLine { get; set; }
        public bool IsFinished { get; set; }
        public bool IsInNest { get; set; }
        public PawnColor Color { get; set; }
        public int PlayerId { get; set; }
    }
}
