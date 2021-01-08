using System.Collections.Generic;
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

        public PlayersFloors GetSingleData(int userId, int floorId)
        {
            var player = _context.PlayersFloors.Find(userId, floorId);
            if (player == null)
            {
                throw new NotFoundException("User or floor do not match, try again");
            }
            return player;
        }

        public PlayersFloors UpdateTable(int userId, int floorId, int value)
        {
            var player = _context.PlayersFloors.Find(userId, floorId);
            if (player == null)
            {
                throw new NotFoundException("User or floor do not match, try again");
            }
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