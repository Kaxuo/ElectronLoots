using System.Collections.Generic;
using Loots.Models;

namespace Loots.Repository.Interface
{
    public interface IPlayers
    {
        IEnumerable<Players> GetAllPlayers();
        IEnumerable<Players> AddPlayers(Players player);
        void DeletePlayer(int id);
        IEnumerable<Players> AddFloors(Floors floor);
    }
}