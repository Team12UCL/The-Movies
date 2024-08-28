using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Movies.Models
{
    public class Biografsal
    {
        public string Id { get; set; }
        public int Seats { get; set; }
        // Hver biografsal har en liste med spilletider
        public List<Spilletid> PlayTimes { get; set; }
    }
}
