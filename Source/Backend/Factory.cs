using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;

namespace Backend
{
    public class Factory
    {
        public static List<Pawn> CreatePawnList()
        {
            return new List<Pawn>();
        }
        public static List<Player> CreatePlayerList()
        {
            return new List<Player>();
        }

        //public static GameSession CreateGameSession()
        //{
        //    return new GameSession();
        //}

        public static Player CreateNewPlayer()
        {
            return new Player();
        }

        public static Pawn CreateNewPawn()
        {
            return new Pawn();
        }
    }
}