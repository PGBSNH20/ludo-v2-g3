using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public interface IPlayer
    {
        int Id { get; set; }
        string Name { get; set; }
        List<Pawn> Pawns { get; set; }
        Guid GameSessionId { get; set; }
    }
}