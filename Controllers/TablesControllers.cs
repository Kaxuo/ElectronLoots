using System;
using System.Collections.Generic;
using BackEnd.Exceptions;
using Loots.Models;
using Loots.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Loots.Controllers
{
    [Route("api/tables")]
    [ApiController]
    public class TablesControllers : ControllerBase
    {
        private readonly ITables _tablesRepository;

        public TablesControllers(ITables tables)
        {
            _tablesRepository = tables;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tables>> GetAllTables()
        {
            var tables = _tablesRepository.GetAllTables();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        public ActionResult<Tables> GetOneTable(int id)
        {
            try
            {
                var singleTable = _tablesRepository.GetOneTable(id);
                return Ok(singleTable);
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

        [HttpPost]
        public ActionResult<Tables> AddTables(Tables table)
        {
            try
            {
                _tablesRepository.AddTables(table);
                return table;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Tables>> DeleteTable(int id)
        {
            try
            {
                _tablesRepository.DeleteTable(id);
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
        [HttpPost("{id}/addplayers")]
        public ActionResult<Players> AddPlayers(int id, Players players)
        {
            try
            {
                _tablesRepository.AddPlayers(id, players);
                return Ok(players);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("{id}/addfloors")]
        public ActionResult<Floors> AddFloors(int id, Floors floors)
        {
            try
            {
                _tablesRepository.AddFloors(id, floors);
                return Ok(floors);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}