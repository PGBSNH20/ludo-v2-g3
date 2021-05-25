using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pawn> Pawns { get; set; }
        public Guid GameSessionId { get; set; }

        public Player()
        {
            Pawns = Factory.CreatePawnList();
        }
    }
}