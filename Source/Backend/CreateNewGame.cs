using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enum;
using Backend.Model;
using Backend.Requests;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Backend
{
    public class CreateNewGame : ICreateNewGame
    {
        private readonly IGameSession _gameSession;

        public CreateNewGame(IGameSession gameSession)
        {
            _gameSession = gameSession;
        }
        public IGameSession Create(NewGameRequest request)
        {
            //TODO: Bort med factory, använd inbyggda DI (Kolla upp scoped på rätt ställen)
            //IGameSession session = Factory.CreateGameSession();
            _gameSession.Name = request.SessionName;
            //session.Name = request.SessionName;

            IPlayer playerOne = Factory.CreateNewPlayer();
            playerOne.Name = request.PlayerOne;
            for (int i = 0; i < 4; i++)
            {
                IPawn greenPawn = Factory.CreateNewPawn();
                greenPawn.Color = PawnColor.Green;
                playerOne.Pawns.Add((Pawn)greenPawn);
            }
            _gameSession.Players.Add((Player)playerOne);

            IPlayer playerTwo = Factory.CreateNewPlayer();
            playerTwo.Name = request.PlayerTwo;
            for (int i = 0; i < 4; i++)
            {
                var yellowPawn = Factory.CreateNewPawn();
                yellowPawn.Color = PawnColor.Yellow;
                playerTwo.Pawns.Add((Pawn)yellowPawn);
            }
            _gameSession.Players.Add((Player)playerTwo);

            if (!string.IsNullOrEmpty(request.PlayerThree))
            {
                IPlayer playerThree = Factory.CreateNewPlayer();
                playerThree.Name = request.PlayerThree;
                for (int i = 0; i < 4; i++)
                {
                    var bluePawn = Factory.CreateNewPawn();
                    bluePawn.Color = PawnColor.Blue;
                    playerThree.Pawns.Add((Pawn)bluePawn);
                }
                _gameSession.Players.Add((Player)playerThree);
            }

            if (!string.IsNullOrEmpty(request.PlayerFour))
            {
                IPlayer playerFour = Factory.CreateNewPlayer();
                playerFour.Name = request.PlayerFour;
                for (int i = 0; i < 4; i++)
                {
                    var redPawn = Factory.CreateNewPawn();
                    redPawn.Color = PawnColor.Red;
                    playerFour.Pawns.Add((Pawn)redPawn);
                }
                _gameSession.Players.Add((Player)playerFour);
            }

            return _gameSession;
        }
    }
}
