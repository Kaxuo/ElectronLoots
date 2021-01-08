using System;
using System.Linq;
using BackEnd.Exceptions;
using Loots.Models;
using Loots.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Loots.Controllers
{
    [Route("api/joinedtable")]
    [ApiController]
    public class PlayersFloorsControllers : ControllerBase
    {
        private readonly IPlayersFloors _values;
        private readonly IPlayers _playersRepository;
        private IFloors _floorsRepository;

        public PlayersFloorsControllers(IPlayersFloors value, IPlayers players, IFloors floors)
        {
            _values = value;
            _playersRepository = players;
            _floorsRepository = floors;
        }

        [HttpGet]
        public ActionResult<PlayersFloors> GetValues()
        {
            var players = _values.GetTable();
            return Ok(players);
        }

        [HttpGet("{userid}/{floorId}")]
        public ActionResult<PlayersFloors> GetSingleRow(int userId, int floorId)
        {
            try
            {
                var players = _values.GetSingleData(userId, floorId);
                return Ok(players);
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

        [HttpPut("{userid}/{floorId}/update")]
        public ActionResult<PlayersFloors> UpdateSingleRow(int userId, int floorId, PlayersFloors newValue)
        {
            try
            {
                var players = _values.UpdateTable(userId, floorId, newValue.Value);
                return Ok(players);
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

        [HttpPost("addplayers")]
        public ActionResult<Floors> AddPlayers(Players player)
        {
            try
            {
                var floors = _floorsRepository.GetAllFloors();
                if (floors == null || !floors.Any())
                {
                    _playersRepository.AddPlayers(player);
                }
                if (floors.Any())
                {
                    _floorsRepository.AddPlayers(player);
                }
                return Ok(floors);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("addfloors")]
        public ActionResult<Players> AddFloors(Floors floor)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}