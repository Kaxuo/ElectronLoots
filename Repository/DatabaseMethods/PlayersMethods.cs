using System.Linq;
using System;
using System.Collections.Generic;
using Loots.Models;
using Loots.Repository.Context;
using Loots.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using BackEnd;
using BackEnd.Exceptions;

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
            var players = _context.Players.Include(s => s.Floors);
            return players;
        }

        public IEnumerable<Players> AddPlayers(Players player)
        {
            if (player == null || player.Name == "")
            {
                throw new AppException("Value cannot be empty");
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
                throw new NotFoundException("Player do not exist");
            }
            _context.Players.Remove(singlePlayer);
            _context.SaveChanges();
        }

        public IEnumerable<Players> AddFloors(Floors floor)
        {
            if (floor == null || floor.Name == "")
            {
                throw new AppException("Don't forget to give a name to the floor");
            }
            var players = _context.Players;
            foreach (Players singlePlayer in players)
            {
                var newFloor = new PlayersFloors { PlayerName = singlePlayer.Name, Players = singlePlayer, FloorName = floor.Name, Floors = floor };
                var single = _context.Players.Find(singlePlayer.userId);
                single.Floors.Add(newFloor);
                _context.Update(single);
                _context.SaveChanges();
            }
            return players;
        }
    }
}