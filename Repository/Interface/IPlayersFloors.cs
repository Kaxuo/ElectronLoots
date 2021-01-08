using System.Collections.Generic;
using Loots.Models;

namespace Loots.Repository.Interface
{
    public interface IPlayersFloors
    {
        IEnumerable<PlayersFloors> GetTable();
        PlayersFloors GetSingleData(int userId, int floorId);
        PlayersFloors UpdateTable(int userId, int floorId, int value);
    }
}