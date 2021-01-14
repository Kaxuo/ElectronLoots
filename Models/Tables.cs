using System.Collections.Generic;

namespace Loots.Models
{
    public class Tables
    {
        public Tables()
        {
            Players = new List<Players>();
            Floors = new List<Floors>();
            PlayersFloors = new List<PlayersFloors>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Players> Players { get; set; }
        public List<Floors> Floors { get; set; }
        public List<PlayersFloors> PlayersFloors { get; set; }
    }
}