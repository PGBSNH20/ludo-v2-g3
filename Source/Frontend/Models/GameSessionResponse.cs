using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public class GameSessionResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; }
        public bool ActiveGame { get; set; }
        public int CurrentPlayer { get; set; }
        public int LatestRoll { get; set; }
        public bool HasRolled { get; set; }
    }
}
