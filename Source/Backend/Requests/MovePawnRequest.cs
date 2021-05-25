using System;

namespace Backend.Controllers
{
    public class MovePawnRequest
    {
        public int PawnId { get; set; }
        public Guid SessionId { get; set; }
    }
}