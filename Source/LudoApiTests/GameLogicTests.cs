using System;
using System.Collections.Generic;
using System.Linq;
using Backend;
using Backend.Controllers;
using Backend.Data;
using Backend.Database;
using Backend.Enum;
using Backend.GameLogics;
using Backend.Model;
using Backend.Requests;
using GameEngine.GameLogic;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace LudoApiTests
{
    public class GameLogicTests
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

        [Fact]
        public void LudoController_Roll_Dice_Expect_Has_Rolled_True()
        {
            List<GameSession> gameSessions = new List<GameSession>();
            IGameSession gameSession = new GameSession();
            ICreateNewGame createNewGame = new CreateNewGame(gameSession);
            DbContextOptions<LudoContext> options = new DbContextOptionsBuilder<LudoContext>().Options;
            var moqContext = new Mock<LudoContext>(options);
            IMovingPawn movingPawn = new MovingPawn(moqContext.Object, _pawnFinishLinePosition, _pawnStartPosition,
                _rotatePlayer, _dbQueries, _findPawn, _knockPawn, _newPawnPosition, _displayMessage, _gameIsActive);
            var ludoController = new LudoController(moqContext.Object, createNewGame, movingPawn, _displayMessage);
            gameSessions.Add((GameSession)gameSession);

            moqContext.Setup(gs => gs.GameSessions).ReturnsDbSet(gameSessions);

            //Act
            ludoController.RollDice(gameSession.Id);

            Assert.True(gameSession.HasRolled);
        }

        [Theory]
        [InlineData(1, 2, 0)]
        [InlineData(1, 4, 2)]
        [InlineData(3, 4, 0)]
        [InlineData(2, 3, 0)]
        [InlineData(1, 3, 2)]
        [InlineData(0, 4, 1)]
        public void Current_Player_Is_X_Rotate_Player_Expect_Current_Player_Y(int currentPlayer, int totalPlayers, int expected)
        {
            int result = _rotatePlayer.GetNewPlayer(currentPlayer, totalPlayers);

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(12, 4, PawnColor.Blue, 0)]
        [InlineData(12, 3, PawnColor.Blue, 15)]
        [InlineData(0, 5, PawnColor.Blue, 5)]
        [InlineData(22, 6, PawnColor.Red, 1)]
        [InlineData(37, 1, PawnColor.Green, 0)]
        [InlineData(37, 4, PawnColor.Green, 3)]
        public void Roll_Is_X_And_EnterFinishLine_At_Y_And_Pawn_Color_Is_Z_Expect_Position_XY(int currentPosition, int latestRoll, PawnColor color, int expected)
        {
            Pawn pawn = new Pawn();
            pawn.Color = color;
            pawn.Position = currentPosition;
            pawn.IsInNest = false;
            pawn.AtFinishLine = false;
            pawn.IsFinished = false;

            int finishLinePosition = _pawnFinishLinePosition.Get(pawn.Color.ToString());

            _newPawnPosition.Calculate(latestRoll, finishLinePosition, pawn);

            Assert.Equal(expected, pawn.Position);
        }

        [Theory]
        [InlineData(17, true)]
        [InlineData(15, false)]
        public void Knock_Pawn_At_Position_X_Expect_IsInNest_Equals_True(int knockPosition, bool expected)
        {
            GameSession gameSession = new GameSession();
            List<Player> players = new List<Player>();
            Player playerOne = Factory.CreateNewPlayer();
            Pawn pawn = new Pawn();
            pawn.Position = 17;
            pawn.IsInNest = false;
            playerOne.Pawns.Add(pawn);
            players.Add(playerOne);
            gameSession.Players.Add(playerOne);

            _knockPawn.ByPosition(knockPosition, gameSession);

            Assert.Equal(pawn.IsInNest, expected);
        }

        [Theory]
        [InlineData(true, true, true, true, false)]
        [InlineData(true, true, true, false, true)]
        [InlineData(false, false, false, false, true)]
        public void When_Four_Pawns_Is_Finished_Expect_GameIsActive_False_Else_True(bool oneIsFinished, bool twoIsFinished, bool threeIsFinished, bool fourIsFinished, bool expected)
        {
            //Arrange
            Pawn pawnOne = Factory.CreateNewPawn();
            Pawn pawnTwo = Factory.CreateNewPawn();
            Pawn pawnThree = Factory.CreateNewPawn();
            Pawn pawnFour = Factory.CreateNewPawn();
            pawnOne.IsFinished = oneIsFinished;
            pawnTwo.IsFinished = twoIsFinished;
            pawnThree.IsFinished = threeIsFinished;
            pawnFour.IsFinished = fourIsFinished;
            List<Pawn> pawns = new List<Pawn>();
            pawns.Add(pawnOne);
            pawns.Add(pawnTwo);
            pawns.Add(pawnThree);
            pawns.Add(pawnFour);

            //Act
            bool actual = _gameIsActive.Check(pawns);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Get_Game_Session_By_Id_Expect_TestSession_As_Session_Name()
        {
            //Arrange
            IGameSession gameSession = new GameSession();
            DbContextOptions<LudoContext> options = new DbContextOptionsBuilder<LudoContext>().Options;
            var moqContext = new Mock<LudoContext>(options);

            List<GameSession> gameSessions = new List<GameSession>();
            gameSession.Name = "TestSession";
            gameSessions.Add((GameSession)gameSession);

            moqContext.Setup(gs => gs.GameSessions).ReturnsDbSet(gameSessions);

            //Act
            var foundSession = _dbQueries.GetGameSessionById(gameSession.Id, moqContext.Object);

            //Assert
            Assert.Equal("TestSession", foundSession.Name);
        }

        [Fact]
        public void Get_Game_Session_By_Id_Expect_Players_Included()
        {
            //Arrange
            IGameSession gameSession = new GameSession();
            DbContextOptions<LudoContext> options = new DbContextOptionsBuilder<LudoContext>().Options;
            var moqContext = new Mock<LudoContext>(options);

            List<GameSession> gameSessions = new List<GameSession>();
            gameSession.Name = "Test Session";
            gameSessions.Add((GameSession)gameSession);
            List<Player> players = new List<Player>();
            var playerOne = Factory.CreateNewPlayer();
            playerOne.GameSessionId = gameSession.Id;
            players.Add(playerOne);
            var playerTwo = Factory.CreateNewPlayer();
            playerTwo.GameSessionId = gameSession.Id;
            players.Add(playerTwo);
            gameSession.Players = players;

            moqContext.Setup(gs => gs.Players).ReturnsDbSet(players);
            moqContext.Setup(gs => gs.GameSessions).ReturnsDbSet(gameSessions);

            //Act
            var foundSession = _dbQueries.GetGameSessionById(gameSession.Id, moqContext.Object);

            //Assert
            Assert.Equal(2, foundSession.Players.Count);
        }

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
