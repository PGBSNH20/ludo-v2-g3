using System;
using System.Collections.Generic;
using Backend;
using Backend.Data;
using Backend.Database;
using Backend.Enum;
using Backend.GameLogics;
using Backend.Model;
using GameEngine.GameLogic;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LudoApiTests
{
    public class UnitTest1
    {
        private IPawnFinishLinePosition _pawnFinishLinePosition = new PawnFinishLinePosition();
        private IPawnStartPosition _pawnStartPosition = new PawnStartPosition();
        private IRotatePlayer _rotatePlayer = new RotatePlayer();
        private IDbQueries _dbQueries = new DbQueries();
        private IFindPawn _findPawn = new FindPawn(new Pawn());
        private IKnockPawn _knockPawn = new KnockPawn();
        private INewPawnPosition _newPawnPosition = new NewPawnPosition();
        private IDisplayMessage _displayMessage = new DisplayMessage();
        private IGameIsActive _gameIsActive = new GameIsActive();

        [Theory]
        [InlineData(1, true)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(9, true)]
        [InlineData(12, true)]
        [InlineData(16, false)]
        [InlineData(-14, false)]
        public void Return_Pawn_With_Id_X_From_List_Of_Players(int pawnId, bool expected)
        {
            List<Player> players = new List<Player>();
            Player jonas = Factory.CreateNewPlayer();
            for (int i = 1; i < 5; i++)
            {
                IPawn bluePawn = Factory.CreateNewPawn();
                bluePawn.ID = i;
                bluePawn.Color = PawnColor.Blue;
                jonas.Pawns.Add((Pawn)bluePawn);
            }
            Player sebbe = Factory.CreateNewPlayer();
            for (int i = 5; i < 9; i++)
            {
                IPawn redPawn = Factory.CreateNewPawn();
                redPawn.ID = i;
                redPawn.Color = PawnColor.Red;
                sebbe.Pawns.Add((Pawn)redPawn);
            }
            Player patric = Factory.CreateNewPlayer();
            for (int i = 9; i < 13; i++)
            {
                IPawn greenPawn = Factory.CreateNewPawn();
                greenPawn.ID = i;
                greenPawn.Color = PawnColor.Green;
                patric.Pawns.Add((Pawn)greenPawn);
            }
            players.Add(jonas);
            players.Add(sebbe);
            players.Add(patric);

            IPawn foundPawn = _findPawn.ById(pawnId, players);
            int foundPawnId = 0;

            if (foundPawn != null)
            {
                foundPawnId = foundPawn.ID;
            }

            Assert.Equal(pawnId == foundPawnId, expected);
        }

        [Theory]
        [InlineData("Green", 38)]
        [InlineData("Red", 27)]
        [InlineData("Yellow", 5)]
        [InlineData("Blue", 16)]
        public void Pawn_Color_Is_X_Expect_Start_Position_Y(string color, int expected)
        {
            int result = _pawnStartPosition.Get(color);
            Assert.Equal(expected, result);
        }
    }
}
