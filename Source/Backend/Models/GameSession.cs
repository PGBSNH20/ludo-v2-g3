using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class GameSession : IGameSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; }
        public bool ActiveGame { get; set; }
        public int CurrentPlayer { get; set; }
        public int LatestRoll { get; set; }
        public bool HasRolled { get; set; }
        public GameSession()
        {
            Id = Guid.NewGuid();
            ActiveGame = true;
            Players = Factory.CreatePlayerList();
            HasRolled = false;
        }
    }
}