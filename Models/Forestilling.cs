﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Movies.Models
{
    public class Forestilling
    {
        public string Cinema { get; set; }
        public string Town { get; set; }
        // Hver forestilling har en biografsals-objekt
        public Biografsal CinemaHall { get; set; }
        public DayOfWeek Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        // Hver forestilling har et film-objekt
        public Film Movie { get; set; }

        // Opsummering af forestillingen
        public string Summary => $"{Movie.Title} - {Cinema} ({Town}), Sal {CinemaHall.Id}, Tid: {Day} {StartTime:HH:mm} til {EndTime:HH:mm}";

    }
}
