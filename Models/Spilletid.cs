﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Movies.Models
{
    public class Spilletid
    {
        // Vi bruger en dansk kultur til at formattere datoer
        private static readonly CultureInfo danishCulture = new CultureInfo("da-DK");

        public DayOfWeek Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Vi binder til en summary property for at kunne vise samlet spilletid data i UI
        public string FormattedTime => StartTime.ToString("dddd d. MMMM yyyy HH:mm", danishCulture);
    }
}
