using Loots.Models;
using Loots.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Loots.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class PlayersFloorsControllers : ControllerBase
    {
        private readonly IPlayersFloors _values;
        public PlayersFloorsControllers(IPlayersFloors value)
        {
            _values = value;
        }

        [HttpGet]
        public ActionResult<PlayersFloors> GetValues()
        {
            var players = _values.GetTable();
            return Ok(players);
        }
    }
}