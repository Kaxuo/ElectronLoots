using System;
using System.Linq;
using BackEnd.Exceptions;
using Loots.Models;
using Loots.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Loots.Controllers
{
    [Route("api/joined")]
    [ApiController]
    public class PlayersFloorsControllers : ControllerBase
    {
        private readonly IPlayersFloors _values;
        private readonly IPlayers _playersRepository;
        private IFloors _floorsRepository;
        private readonly ITables _tablesRepository;

        public PlayersFloorsControllers(IPlayersFloors value, IPlayers players, IFloors floors, ITables tables)
        {
            _values = value;
            _playersRepository = players;
            _floorsRepository = floors;
            _tablesRepository = tables;
        }

        [HttpGet]
        public ActionResult<PlayersFloors> GetValues()
        {
            var players = _values.GetTable();
            return Ok(players);
        }

        [HttpGet("{tableId}/{userid}/{floorId}")]
        public ActionResult<PlayersFloors> GetSingleRow(int tableId, int userId, int floorId)
        {
            try
            {
                var players = _values.GetSingleData(tableId, userId, floorId);
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

        [HttpPut("{tableId}/{userid}/{floorId}")]
        public ActionResult<PlayersFloors> UpdateSingleRow(int tableId, int userId, int floorId, PlayersFloors newValue)
        {
            try
            {
                var players = _values.UpdateTable(tableId, userId, floorId, newValue.Value);
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
    }
}