using System.Collections.Generic;
using Loots.Models;
using Loots.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Loots.Controllers
{
    [Route("api/floors")]
    [ApiController]
    public class FloorsControllers : ControllerBase
    {
        private readonly IFloors _floorsRepository;
        public FloorsControllers(IFloors floors)
        {
            _floorsRepository = floors;
        }

        [HttpGet]
        public ActionResult<object> GetPlayers()
        {
            var floors = _floorsRepository.GetAllFloors();
            return Ok(floors);
        }

        [HttpPost("add")]
        public ActionResult<Floors> AddPlayer(Floors floor)
        {
            var floors = _floorsRepository.AddFloors(floor);
            return Ok(floors);
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Floors>> DeletePlayer(int id)
        {
            _floorsRepository.DeleteFloor(id);
            return Ok();
        }

    }
}