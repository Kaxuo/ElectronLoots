using System.Security;
using System.Collections.Generic;
using BackEnd;
using BackEnd.Exceptions;
using Loots.Models;
using Loots.Repository.Context;
using Loots.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Loots.Repository.DatabaseMethods
{
    public class TablesMethods : ITables
    {
        private readonly PlayersContext _context;

        public TablesMethods(PlayersContext context)
        {
            _context = context;
        }
        public IEnumerable<Tables> GetAllTables()
        {
            var tables = _context.Tables.Include(s => s.Players).Include(s => s.Floors).Include(s => s.PlayersFloors);
            return tables;
        }

        public Tables AddTables(Tables table)
        {
            if (table == null || table.Name == "")
            {
                throw new AppException("Value cannot be empty");
            }
            _context.Tables.Add(table);
            _context.SaveChanges();
            var tables = _context.Tables;
            return table;
        }
        public Tables GetOneTable(int id)
        {
            var singleTable = _context.Tables.Include(t => t.Floors).Include(t => t.Players).Include(t => t.PlayersFloors).FirstOrDefault(x => x.Id == id);
            if (singleTable == null)
            {
                throw new NotFoundException("Table do not exist");
            }
            return singleTable;
        }
        public void DeleteTable(int id)
        {
            var singleTable = _context.Tables.Find(id);
            if (singleTable == null)
            {
                throw new NotFoundException("Table do not exist");
            }
            _context.Tables.Remove(singleTable);
            _context.SaveChanges();
        }

        public Players AddPlayers(int id, Players player)
        {
            if (player == null || player.Name == "")
            {
                throw new AppException("Name is empty");
            }
            var singleTable = _context.Tables.Include(s => s.Players).Include(s => s.Floors).Include(s => s.PlayersFloors).FirstOrDefault(x => x.Id == id);
            if (singleTable == null)
            {
                throw new NotFoundException("Table do not exist");
            }

            if (singleTable.Floors == null || !singleTable.Floors.Any())
            {
                singleTable.Players.Add(player);
                _context.Update(singleTable);
                _context.SaveChanges();
                return player;
            }
            else
            {
                singleTable.Players.Add(player);
                foreach (Floors singleFloor in singleTable.Floors)
                {
                    var newPlayer = new PlayersFloors { PlayerName = player.Name, Players = player, FloorName = singleFloor.Name, Floors = singleFloor };
                    var single = _context.Floors.Find(singleFloor.FloorId);
                    single.Players.Add(newPlayer);
                    singleTable.PlayersFloors.Add(newPlayer);
                    _context.Update(single);
                    _context.SaveChanges();
                }
                return player;
            }

        }

        public Floors AddFloors(int id, Floors floor)
        {
            if (floor == null || floor.Name == "")
            {
                throw new AppException("Name is empty");
            }
            var singleTable = _context.Tables.Include(s => s.Players).Include(s => s.Floors).Include(s => s.PlayersFloors).FirstOrDefault(x => x.Id == id);
            if (singleTable == null)
            {
                throw new NotFoundException("Table do not exist");
            }
            if (singleTable.Players == null || !singleTable.Players.Any())
            {
                singleTable.Floors.Add(floor);
                _context.Update(singleTable);
                _context.SaveChanges();
                return floor;
            }
            else
            {
                singleTable.Floors.Add(floor);
                foreach (Players singlePlayer in singleTable.Players)
                {
                    var newFloor = new PlayersFloors { PlayerName = singlePlayer.Name, Players = singlePlayer, FloorName = floor.Name, Floors = floor };
                    var single = _context.Players.Find(singlePlayer.UserId);
                    single.Floors.Add(newFloor);
                    singleTable.PlayersFloors.Add(newFloor);
                    _context.Update(single);
                    _context.SaveChanges();
                }
                return floor;
            }
        }
    }
}