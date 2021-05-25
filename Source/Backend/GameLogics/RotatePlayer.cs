using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameLogic
{
    public class RotatePlayer : IRotatePlayer
    {
        public int GetNewPlayer(int currentPlayer, int totalPlayers)
        {
            if (currentPlayer == totalPlayers - 1)
            {
                currentPlayer = 0;
            }
            else
            {
                currentPlayer++;
            }

            return currentPlayer;
        }
    }
}