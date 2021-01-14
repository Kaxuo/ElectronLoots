
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Loots.Models
{
    public class Floors
    {
        public Floors()
        {
            Players = new List<PlayersFloors>();
        }
        [Key]
        public int FloorId { get; set; }
        public string Name { get; set; }
        public List<PlayersFloors> Players { get; set; }
        public int TableId { get; set; }
        [JsonIgnore]
        public Tables Tables { get; set; }
    }
}