using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace Frontend.Pages
{
    public class JoinGameModel : PageModel
    {
        public GameSessionResponse GameSession;
        [BindProperty(SupportsGet = true)]
        public Guid SessionId { get; set; }

        public IActionResult OnPostTest()
        {
            var client = new RestClient("https://localhost:44303/api");
            client.Timeout = -1;
            var request = new RestRequest($"/Ludo/{SessionId}", DataFormat.Json);
            var response = client.Get<GameSessionResponse>(request);

            var url = $"/Ludo/{response.Data.Id}";

            return Redirect(url);


        }
    }
}
