using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Hubs
{
    public class LudoHub : Hub
    {
        public async Task SendDiceRoll(int roll)
        {
            await base.Clients.All.SendAsync("RecieveDiceRoll", roll);
        }
    }
}
