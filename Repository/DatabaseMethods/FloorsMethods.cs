using System;
using System.Collections.Generic;
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

        public FloorsMethods()
        {
        }

        public IEnumerable<Floors> GetAllFloors()
        {
            var floors = _context.Floors.Include(s => s.Players);
            return floors;
        }
        public IEnumerable<Floors> AddFloors(Floors floor)
        {
            if (floor == null)
            {
                throw new ArgumentNullException(nameof(floor));
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
                throw new ArgumentNullException("Not found");
            }
            _context.Floors.Remove(floor);
            _context.SaveChanges();
        }

        // public Floors AddPlayers(Players player)
        // {
        //     var singleFloor = _context.Floors.Find(1);
        //     singleFloor.Players.Add(player);
        //     _context.Update(singleFloor);
        //     _context.SaveChanges();
        //     return singleFloor;
        // }
    }
}