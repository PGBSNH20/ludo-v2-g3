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
    public class CreateGameModel : PageModel
    {
        [BindProperty] public AddNewGame NewGame { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var client = new RestClient("https://localhost:44303/api/Ludo/NewGame");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(NewGame);
            IRestResponse response = client.Execute(request);
            var sessionIdResponse = response.Content;

            var sessionId = sessionIdResponse.Substring(1, 36);
            var url = $"/ludo/{sessionId}";
            return Redirect(url);
        }
    }
}