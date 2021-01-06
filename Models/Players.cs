using System.Collections.Generic;

namespace Loots.Models
{
    public class Players
    {
        public Players()
        {
            Floors = new List<Floors>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Floors> Floors { get; set; }
    }
}
