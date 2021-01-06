using System.Collections.Generic;
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
        public ActionResult<object> GetPlayers()
        {
            var players = _playersRepository.GetAllPlayers();
            return Ok(players);
        }

        [HttpPost("add")]
        public ActionResult<Players> AddPlayer(Players player)
        {
            var players = _playersRepository.AddPlayer(player);
            return Ok(players);
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Players>> DeletePlayer(int id)
        {
            _playersRepository.DeletePlayer(id);
            return Ok();
        }
    }
}