using System;
using System.Collections.Generic;
using BackEnd.Exceptions;
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
        public ActionResult<Floors> GetPlayers()
        {
            var floors = _floorsRepository.GetAllFloors();
            return Ok(floors);
        }

        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Floors>> DeleteFloor(int id)
        {
            try
            {
                _floorsRepository.DeleteFloor(id);
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