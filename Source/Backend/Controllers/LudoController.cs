using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Data;
using Backend.Enum;
using Backend.GameLogics;
using Backend.Model;
using Backend.Requests;
using GameEngine.GameLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LudoController : ControllerBase
    {
        private readonly LudoContext _dbContext;
        private readonly ICreateNewGame _createNewGame;
        private readonly IMovingPawn _movingPawn;
        private readonly IDisplayMessage _displayMessage;

        public LudoController(LudoContext dbContext, ICreateNewGame createNewGame, IMovingPawn movingPawn, IDisplayMessage displayMessage)
        {
            _dbContext = dbContext;
            _createNewGame = createNewGame;
            _movingPawn = movingPawn;
            _displayMessage = displayMessage;
        }

        // GET: api/<LudoController>
        [HttpPut("[action]")]
        public IActionResult RollDice([FromBody] Guid id)
        {
            var foundSession = _dbContext.GameSessions.FirstOrDefault(gs => gs.Id == id);

            if (foundSession == null)
            {
                return BadRequest(_displayMessage.NoGameId());
            }

            if (foundSession.HasRolled)
            {
                return Ok(_displayMessage.HasRolled());
            }

            Random rnd = new Random();
            int roll = rnd.Next(1, 7);

            foundSession.LatestRoll = roll;
            foundSession.HasRolled = true;
            _dbContext.SaveChanges();
            return Ok(roll);
        }

        // GET api/<LudoController>/5
        [HttpGet("{id}")]
        public IActionResult GameSession(Guid id)
        {
            IGameSession session = _dbContext.GameSessions
                .Include(gs => gs.Players)
                .ThenInclude(p => p.Pawns)
                .FirstOrDefault(gs => gs.Id == id);

            if (session != null)
            {
                return Ok(session);
            }
            return BadRequest(_displayMessage.SessionNotFound());
        }

        // POST api/<LudoController>
        [HttpPost("[action]")]
        public IActionResult NewGame([FromBody] NewGameRequest request)
        {
            //TODO: Namngivning klasser och metoder (SOLID)
            IGameSession session = _createNewGame.Create(request);
            try
            {
                _dbContext.GameSessions.Add((GameSession)session);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, session.Id);
            }
            catch
            {
                return Conflict(_displayMessage.DbError());
            }
        }

        // PUT api/<LudoController>/5
        [HttpPut("[action]")]
        public IActionResult MovePawn([FromBody] MovePawnRequest request)
        {
            return Ok(_movingPawn.Move(request));
        }

        // DELETE api/<LudoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
