using System.Collections.Generic;
using Loots.Models;

namespace Loots.Repository.Interface
{
    public interface ITables
    {
        IEnumerable<Tables> GetAllTables();
        Tables AddTables(Tables tables);

        Tables GetOneTable(int id);
        void DeleteTable(int id);
        Players AddPlayers(int id, Players player);
        Floors AddFloors(int id, Floors floor);
    }
}