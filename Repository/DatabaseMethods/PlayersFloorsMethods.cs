using System.Collections.Generic;
using System.Linq;
using BackEnd;
using BackEnd.Exceptions;
using Loots.Models;
using Loots.Repository.Context;
using Loots.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loots.Repository.DatabaseMethods
{
    public class PlayersFloorsMethods : IPlayersFloors
    {
        private readonly PlayersContext _context;
        public PlayersFloorsMethods(PlayersContext context)
        {
            _context = context;
        }

        public IEnumerable<PlayersFloors> GetTable()
        {
            var players = _context.PlayersFloors.Include(s => s.Floors);
            return players;
        }

        public PlayersFloors GetSingleData(int tableId, int userId, int floorId)
        {
            var singleTable = _context.Tables.Find(tableId);
            if (singleTable == null)
            {
                throw new NotFoundException("Table/User/floor do not match, try again");
            }
            var table = _context.Tables.Include(t => t.PlayersFloors).FirstOrDefault(t => t.Id == tableId);
            var player = table.PlayersFloors.FirstOrDefault(x => x.UserId == userId && x.FloorId == floorId);
            return player;
        }

        public PlayersFloors UpdateTable(int tableId, int userId, int floorId, int value)
        {
            var singleTable = _context.Tables.Find(tableId);
            if (singleTable == null)
            {
                throw new NotFoundException("Table/User/floor do not match, try again");
            }
            var table = _context.Tables.Include(t => t.PlayersFloors).FirstOrDefault(t => t.Id == tableId);
            var player = table.PlayersFloors.FirstOrDefault(x => x.UserId == userId && x.FloorId == floorId);
            if (value < 0)
            {
                player.Value = 0;
            }
            else
            {
                player.Value = value;
            }
            _context.PlayersFloors.Update(player);
            _context.SaveChanges();
            return player;
        }
    }
}