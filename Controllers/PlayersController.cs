using System.Collections.Generic;
using System.Linq;
using Loots.Models;
using Loots.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Loots.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IFloors _floorsRepository;
        private readonly IPlayers _playersRepository;
        public PlayersController(IPlayers players, IFloors floors)
        {
            _playersRepository = players;
            _floorsRepository = floors;
        }

        [HttpGet]
        public ActionResult<Players> GetPlayers()
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

        [HttpPost("addfloors")]
        public ActionResult<Players> AddFloors(Floors floor)
        {
            var players = _playersRepository.GetAllPlayers();
            if (players == null || !players.Any())
            {
                _floorsRepository.AddFloors(floor);
            }
            if (players.Any())
            {
                _playersRepository.AddFloors(floor);
            }
            return Ok(players);
        }
    }
}