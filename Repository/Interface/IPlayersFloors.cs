using System.Collections.Generic;
using Loots.Models;

namespace Loots.Repository.Interface
{
    public interface IPlayersFloors
    {
        IEnumerable<PlayersFloors> GetTable();
        IEnumerable<PlayersFloors> UpdateTable();
    }
}