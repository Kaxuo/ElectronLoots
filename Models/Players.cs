using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Loots.Models
{
    public class Players
    {
        public Players()
        {
            Floors = new List<PlayersFloors>();
        }
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<PlayersFloors> Floors { get; set; }
        public int TableId { get; set; }
        [JsonIgnore]
        public Tables Tables { get; set; }
    }
}
