using System;
using Backend.Data;
using Backend.Model;

namespace Backend.Database
{
    public interface IDbQueries
    {
        IGameSession GetGameSessionById(Guid id, LudoContext context);
    }
}