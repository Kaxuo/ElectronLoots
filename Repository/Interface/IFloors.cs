using System.Collections.Generic;
using Loots.Models;

namespace Loots.Repository.Interface
{
    public interface IFloors
    {
        IEnumerable<Floors> GetAllFloors();
        IEnumerable<Floors> AddFloors(Floors floor);
        void DeleteFloor(int id);
        IEnumerable<Floors> AddPlayers(Players player);
    }
}