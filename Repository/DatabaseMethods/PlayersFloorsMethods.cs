using System.Collections.Generic;
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
        public IEnumerable<PlayersFloors> UpdateTable()
        {
            throw new System.NotImplementedException();
        }
    }
}