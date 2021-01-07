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
        [KeyAttribute]
        public int userId { get; set; }
        public string Name { get; set; }
        public List<PlayersFloors> Floors { get; set; }
    }
}
