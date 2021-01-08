using System;
using System.Collections.Generic;
using BackEnd.Exceptions;
using Loots.Models;
using Loots.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Loots.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayers _playersRepository;
        public PlayersController(IPlayers players)
        {
            _playersRepository = players;
        }

        [HttpGet]
        public ActionResult<Players> GetPlayers()
        {
            var players = _playersRepository.GetAllPlayers();
            return Ok(players);
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Players>> DeletePlayer(int id)
        {
            try
            {
                _playersRepository.DeletePlayer(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}