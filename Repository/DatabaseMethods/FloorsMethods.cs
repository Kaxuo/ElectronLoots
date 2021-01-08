using System;
using System.Collections.Generic;
using BackEnd;
using BackEnd.Exceptions;
using Loots.Models;
using Loots.Repository.Context;
using Loots.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loots.Repository.DatabaseMethods
{
    public class FloorsMethods : IFloors
    {
        private readonly PlayersContext _context;
        public FloorsMethods(PlayersContext context)
        {
            _context = context;
        }

        public IEnumerable<Floors> GetAllFloors()
        {
            var floors = _context.Floors.Include(s => s.Players);
            return floors;
        }

        public IEnumerable<Floors> AddFloors(Floors floor)
        {
            if (floor == null || floor.Name == "")
            {
                throw new AppException("Value cannot be empty");
            }
            _context.Floors.Add(floor);
            _context.SaveChanges();
            var floors = _context.Floors;
            return floors;
        }
        
        public void DeleteFloor(int id)
        {
            var floor = _context.Floors.Find(id);
            if (floor == null)
            {
                throw new NotFoundException("Floor do not exist");
            }
            _context.Floors.Remove(floor);
            _context.SaveChanges();
        }

        public IEnumerable<Floors> AddPlayers(Players player)
        {
            if (player == null || player.Name == "")
            {
                throw new AppException("Name is empty");
            }
            var floors = _context.Floors;
            foreach (Floors singleFloor in floors)
            {
                var newPlayer = new PlayersFloors { PlayerName = player.Name, Players = player, FloorName = singleFloor.Name, Floors = singleFloor };
                var single = _context.Floors.Find(singleFloor.Id);
                single.Players.Add(newPlayer);
                _context.Update(single);
                _context.SaveChanges();
            }
            return floors;
        }
    }
}