using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Movies.Models
{
    public class Booking
    {
        public string CustomerEmail { get; set; }
        public string CustomerPhone {  get; set; }
        public int NumberOfTickets { get; set; }
        public Forestilling BookedForestilling { get; set;}
        public string Summary => $"{CustomerEmail} ({NumberOfTickets} {BookedForestilling.Cinema} {BookedForestilling.Movie.Summary})";

        public Booking()
        {
            BookedForestilling = new Forestilling();
            BookedForestilling.Movie = new Film();
        }

    }
}
