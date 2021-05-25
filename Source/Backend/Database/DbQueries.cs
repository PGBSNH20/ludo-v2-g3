using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Database
{
    public class DbQueries : IDbQueries
    {
        public IGameSession GetGameSessionById(Guid id, LudoContext context)
        {
            return context.GameSessions.Include(gs => gs.Players).ThenInclude(p => p.Pawns)
                .FirstOrDefault(gs => gs.Id == id);
        }
    }
}