using Loots.Models;
using Loots.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Loots.Controllers
{
    [Route("api/floors")]
    [ApiController]
    public class FloorsControllers : ControllerBase
    {
        private readonly IPlayers _playersRepository;
        private readonly IFloors _floorsRepository;
        public FloorsControllers(IPlayers players, IFloors floors)
        {
            _playersRepository = players;
            _floorsRepository = floors;
        }

        [HttpGet]
        public ActionResult<Floors> GetPlayers()
        {
            var floors = _floorsRepository.GetAllFloors();
            return Ok(floors);
        }
        // [HttpPost("addplayers")]
        // public ActionResult<Players> AddPlayers(Players player)
        // {
        //     var floor = _floorsRepository.AddPlayers(player);
        //     return Ok(floor);
        // }
    }
}