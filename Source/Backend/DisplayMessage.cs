using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend
{
    public class DisplayMessage : IDisplayMessage
    {
        public string MustRollToMove()
        {
            return "You must roll the dice in order to move a pawn.";
        }

        public string NoGameId()
        {
            return "No game with that ID was found.";
        }

        public string HasRolled()
        {
            return "Player has already rolled the dice.";
        }

        public string SessionNotFound()
        {
            return "Session with that id doesn't exist";
        }

        public string DbError()
        {
            return "An error occurred while adding a new game to the database.";
        }

        public string MustRollDice()
        {
            return "You must roll the dice in order to move a pawn.";
        }

        public string PawnNotFound()
        {
            return "Pawn was not found.";
        }

        public string NoAvailablePawns()
        {
            return "No available moves, switching to next player";
        }

        public string PawnHasMoved()
        {
            return "Pawn has been moved.";
        }

        public string OccupiedPosition()
        {
            return "You already have a pawn at that position.";
        }
    }
}