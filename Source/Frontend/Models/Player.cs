using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pawn> Pawns { get; set; }
        public Guid GameSessionId { get; set; }
    }
}
