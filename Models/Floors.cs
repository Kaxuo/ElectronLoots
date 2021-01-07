
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
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PlayersFloors> Players { get; set; }
    }
}