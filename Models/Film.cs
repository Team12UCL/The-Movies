using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Movies.Models
{
    public class Film
    {
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public DateTime PremiereDate { get; set; } = DateTime.Now;
        // Vi binder til en summary property for at kunne vise samlet film data i UI
        public string Summary => $"{Title} ({Duration.TotalMinutes:N0}m, {Genre}) {PremiereDate:dd/mm/yy)}";
    }
}
