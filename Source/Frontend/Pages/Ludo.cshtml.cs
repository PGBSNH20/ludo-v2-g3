using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Frontend.Pages
{
    public class LudoModel : PageModel
    {
        private readonly ILogger<LudoModel> _logger;
        public GameSessionResponse GameSession;

        public LudoModel(ILogger<LudoModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(Guid id)
        {
            var client = new RestClient("https://localhost:44303/api");
            client.Timeout = -1;
            var request = new RestRequest($"/Ludo/{id}", DataFormat.Json);

            var response = client.Get<GameSessionResponse>(request);

            GameSession = response.Data;
        }

        public void RollDice()
        {

        }
    }
}

