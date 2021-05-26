using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Model;
using GameEngine.GameLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Controllers;
using Backend.Database;

namespace Backend.GameLogics
{
    public class MovingPawn : IMovingPawn
    {
        private readonly LudoContext _dbContext;
        private readonly IPawnFinishLinePosition _pawnFinishLinePosition;
        private readonly IPawnStartPosition _pawnStartPosition;
        private readonly IRotatePlayer _rotatePlayer;
        private readonly IDbQueries _dbQueries;
        private readonly IFindPawn _findPawn;
        private readonly IKnockPawn _knockPawn;
        private readonly INewPawnPosition _newPawnPosition;
        private readonly IPawn _pawn;
        private readonly IDisplayMessage _displayMessage;

        public MovingPawn(LudoContext dbContext, IPawnFinishLinePosition pawnFinishLinePosition, IPawnStartPosition pawnStartPosition, IRotatePlayer rotatePlayer,
            IDbQueries dbQueries, IFindPawn findPawn, IKnockPawn knockPawn, INewPawnPosition newPawnPosition, IPawn pawn, IDisplayMessage displayMessage)
        {
            _dbContext = dbContext;
            _pawnFinishLinePosition = pawnFinishLinePosition;
            _pawnStartPosition = pawnStartPosition;
            _rotatePlayer = rotatePlayer;
            _dbQueries = dbQueries;
            _findPawn = findPawn;
            _knockPawn = knockPawn;
            _newPawnPosition = newPawnPosition;
            _pawn = pawn;
            _displayMessage = displayMessage;
        }


        public string Move(MovePawnRequest request)
        {
            //DbQueries dbQueries = new DbQueries();
            var gameSession = _dbQueries.GetGameSessionById(request.SessionId, _dbContext);

            if (!gameSession.HasRolled)
            {
                return _displayMessage.MustRollToMove();
            }

            //FindPawn findPawn = new FindPawn();
            IPawn pawn = _findPawn.ById(request.PawnId, gameSession.Players);

            if (pawn == null)
            {
                return _displayMessage.PawnNotFound();
            }

            // Get all the players pawns in order to check if a move is valid.
            List<Pawn> playerPawns = _findPawn.ByColor(pawn.Color, gameSession.Players);

            if (pawn.IsInNest && gameSession.LatestRoll != 6)
            {
                gameSession.HasRolled = false;
                //TODO: Add interface and DPI
                gameSession.CurrentPlayer = _rotatePlayer.GetNewPlayer(gameSession.CurrentPlayer, gameSession.Players.Count);
                _dbContext.SaveChanges();
                return _displayMessage.NoAvailablePawns();
            }

            if (pawn.IsInNest && gameSession.LatestRoll == 6)
            {
                int startPosition = _pawnStartPosition.Get(pawn.Color.ToString());

                bool invalidMove = playerPawns.Any(p => p.Position == startPosition && p.IsFinished == false);

                if (!invalidMove)
                {
                    //KnockPawn knockPawn = new KnockPawn();
                    _knockPawn.ByPosition(startPosition, gameSession);

                    pawn.Position = startPosition;
                    pawn.IsInNest = false;
                    gameSession.HasRolled = false;
                    gameSession.CurrentPlayer = _rotatePlayer.GetNewPlayer(gameSession.CurrentPlayer, gameSession.Players.Count);
                    _dbContext.SaveChanges();
                    return _displayMessage.PawnHasMoved();
                }
                else
                {
                    return _displayMessage.OccupiedPosition();
                }
            }
            //Last square == 44
            if (!pawn.IsInNest && !pawn.IsFinished)
            {
                //PawnFinishLinePosition pawnFinishLinePosition = new PawnFinishLinePosition();

                var enterFinishLine = _pawnFinishLinePosition.Get(pawn.Color.ToString());

                //NewPawnPosition newPawnPosition = new NewPawnPosition();
                _newPawnPosition.Calculate(gameSession.LatestRoll, enterFinishLine, pawn);

                bool sameColorOccupation = playerPawns.Any(p => p.Position == pawn.Position && p.IsFinished == false && p.ID != pawn.ID);

                if (sameColorOccupation)
                {
                    return _displayMessage.OccupiedPosition();
                }

                foreach (var foundPawn in from player in gameSession.Players from pPawn in player.Pawns where pPawn.Position == pawn.Position && pPawn.Color != pawn.Color && pPawn.IsFinished == false select pPawn)
                {
                    IPawn pawnToKnock = foundPawn;
                    if (pawnToKnock == null) continue;
                    pawnToKnock.Position = 0;
                    pawnToKnock.IsInNest = true;
                }
                gameSession.HasRolled = false;
                //RotatePlayer rotatePlayer = new RotatePlayer();
                gameSession.CurrentPlayer = _rotatePlayer.GetNewPlayer(gameSession.CurrentPlayer, gameSession.Players.Count);
            }

            _dbContext.SaveChanges();
            return _displayMessage.PawnHasMoved();
        }
    }
}
