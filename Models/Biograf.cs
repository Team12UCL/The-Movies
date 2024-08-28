using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Movies.Models
{
    public class Biograf
    {
        public string CinemaName { get; set; }
        public string Town { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        // Hver biograf har en liste med biografsale
        public List<Biografsal> CinemaHalls { get; set; }
        public string Summary => $"{CinemaName} ({Town})";
    }
}
