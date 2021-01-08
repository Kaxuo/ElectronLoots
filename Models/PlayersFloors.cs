using System.Text.Json.Serialization;

namespace Loots.Models
{
    public class PlayersFloors
    {
        public int UserId { get; set; }
        public string PlayerName { get; set; }
        [JsonIgnore]
        public Players Players { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        [JsonIgnore]
        public Floors Floors { get; set; }
        public int Value { get; set; } = 0;
    }
}