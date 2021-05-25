using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enum;

namespace Backend.Model
{
    public class Pawn : IPawn
    {
        public Pawn()
        {
            AtFinishLine = false;
            IsFinished = false;
            Position = 0;
            IsInNest = true;
        }

        public int ID { get; set; }
        public int Position { get; set; }
        public bool AtFinishLine { get; set; }
        public bool IsFinished { get; set; }
        public bool IsInNest { get; set; }
        public PawnColor Color { get; set; }
        public int PlayerId { get; set; }

        public Pawn Create()
        {
            return new Pawn();
        }
    }

}