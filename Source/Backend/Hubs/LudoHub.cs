using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Backend.Model;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Hubs
{
    public class LudoHub : Hub
    {
        private static Dictionary<string, string> ConnectedUsers = new Dictionary<string, string>();
        private LudoContext _dbContext;

        public LudoHub(LudoContext db)
        {
            _dbContext = db;
        }
        
        public async Task SendDiceRoll(int roll, string ludoId)
        {
            IGameSession session = _dbContext.GameSessions
                .Include(gs => gs.Players)
                .First(x => x.Id == Guid.Parse(ludoId));

            Player player = 
                session.CurrentPlayer == 0 ? 
                    session.Players.Last() : 
                    session.Players[session.CurrentPlayer - 1];

            await Clients.Group(ludoId).SendAsync("RecieveDiceRoll", roll, player.Name);
        }

        public async Task UpdateNextPlayerTurn(string ludoId)
        {
            IGameSession session = _dbContext.GameSessions
                .Include(gs => gs.Players)
                .First(x => x.Id == Guid.Parse(ludoId));
            Player playerTurn = session.Players[session.CurrentPlayer];

            await Clients.Group(ludoId.ToString()).SendAsync("UpdatePlayerTurn", playerTurn.Name);
        }

        public async Task JoinRoom(string name, string ludoId)
        {
            if (!ConnectedUsers.ContainsKey(Context.ConnectionId))
            {
                ConnectedUsers.Add(Context.ConnectionId, name);
                await Groups.AddToGroupAsync(Context.ConnectionId, ludoId.ToString());
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedUsers.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SynchronizeGameState(string ludoId)
        {
            IGameSession session = _dbContext.GameSessions
                .Include(gs => gs.Players)
                .ThenInclude(gs => gs.Pawns)
                .FirstOrDefault(x => x.Id == Guid.Parse(ludoId));

            var payload = JsonConvert.SerializeObject(session.Players.Select(x => x.Pawns));

            await Clients.Group(ludoId).SendAsync("UpdateGameState", payload);
        }
    }
}
