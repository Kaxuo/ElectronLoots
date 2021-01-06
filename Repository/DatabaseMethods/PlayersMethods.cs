using System;
using System.Collections.Generic;
using Loots.Models;
using Loots.Repository.Context;
using Loots.Repository.Interface;

namespace Loots.Repository.DatabaseMethods
{
    public class PlayersMethods : IPlayers
    {
        private readonly PlayersContext _context;

        public PlayersMethods(PlayersContext context)
        {
            _context = context;
        }
        public IEnumerable<Players> GetAllPlayers()
        {
            var players = _context.Players;
            return players;
        }
        public IEnumerable<Players> AddPlayer(Players player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }
            _context.Players.Add(player);
            _context.SaveChanges();
            var players = _context.Players;
            return players;
        }
        public void DeletePlayer(int id)
        {
            var singlePlayer = _context.Players.Find(id);
            if (singlePlayer == null)
            {
                throw new ArgumentNullException("Not found");
            }
            _context.Players.Remove(singlePlayer);
            _context.SaveChanges();
        }
    }
}