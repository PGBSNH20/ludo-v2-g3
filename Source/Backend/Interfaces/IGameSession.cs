using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public interface IGameSession
    {
        Guid Id { get; set; }
        string Name { get; set; }
        List<Player> Players { get; set; }
        bool ActiveGame { get; set; }
        int CurrentPlayer { get; set; }
        int LatestRoll { get; set; }
        bool HasRolled { get; set; }
    }
}