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
        // Hvor mange billetter der bliver solgt/booket
        public int NumberOfTickets { get; set; }
        // Hver booking har et forestillings-objekt
        public Forestilling BookedForestilling { get; set;}
        // Vi binder til en summary property for at kunne vise samlet booking data i UI
        public string Summary => $"Id:{CustomerEmail} Antal Billetter: {NumberOfTickets} i Biograf: {BookedForestilling.Cinema} ({BookedForestilling.Town}) til Film: {BookedForestilling.Movie.Title} kl: {BookedForestilling.StartTime:HH:mm} ";

        public Booking()
        {
            BookedForestilling = new Forestilling();
            BookedForestilling.Movie = new Film();
        }

    }
}
