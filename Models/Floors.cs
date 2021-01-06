
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Loots.Models
{
    public class Floors
    {
        public Floors()
        {
            Players = new List<Players>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Players> Players { get; set; }
    }
}