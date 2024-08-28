using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Movies.Models;

namespace The_Movies.Repositories
{
    public class BiografRepository
    {
        public List<Biograf> Biografer { get; set; }

        public BiografRepository()
        {
            // testdata (3 biografer med hver deres CinemaHalls og PlayTimes)
            Biografer = new List<Biograf>
            {
                new Biograf
                {
                    CinemaName = "Nordisk Film",
                    Town = "Aarhus",
                    PhoneNumber = "12345678",
                    Email = "service@nordiskfilm.dk",
                    CinemaHalls = new List<Biografsal>
                    {
                        new Biografsal
                        {
                            Id = "1",
                            PlayTimes = new List<Spilletid>(PopulatePlayTimes())
                        },
                        new Biografsal
                        {
                            Id = "2",
                            PlayTimes = new List<Spilletid>(PopulatePlayTimes())
                        }
                    }
                },

                new Biograf
                {
                    CinemaName = "Cinemaxx",
                    Town = "Randers",
                    PhoneNumber = "12345678",
                    Email = "info@cinemaxx.dk",
                   CinemaHalls = new List<Biografsal>
                    {
                        new Biografsal
                        {
                            Id = "1",
                            PlayTimes = new List<Spilletid>(PopulatePlayTimes())
                        },
                        new Biografsal
                        {
                            Id = "2",
                            PlayTimes = new List<Spilletid>(PopulatePlayTimes())
                        },
                        new Biografsal
                        {
                            Id = "3",
                            PlayTimes = new List<Spilletid>(PopulatePlayTimes())
                        }
                    }
                },

                new Biograf
                {
                    CinemaName = "Palads",
                    Town = "Odense",
                    PhoneNumber = "12345678",
                    Email = "kontakt@palads.dk",
                    CinemaHalls = new List<Biografsal>
                    {
                        new Biografsal
                        {
                            Id = "1",
                            PlayTimes = new List<Spilletid>(PopulatePlayTimes())
                        },
                        new Biografsal
                        {
                            Id = "2",
                            PlayTimes = new List<Spilletid>(PopulatePlayTimes())
                        }
                    }
                }
            };
        }

        public List<Spilletid> PopulatePlayTimes()
        {
            var playTimes = new List<Spilletid>();
            DateTime today = DateTime.Today;

            // laver spilletider for de næste 7 dage til hver biografsal klokken 12:00, 16:00 og 21:00
            for (int i = 0; i < 7; i++)
            {
                DateTime currentDay = today.AddDays(i);
                playTimes.Add(new Spilletid { Day = currentDay.DayOfWeek, StartTime = currentDay.Date.AddHours(12) }); // 12:00
                playTimes.Add(new Spilletid { Day = currentDay.DayOfWeek, StartTime = currentDay.Date.AddHours(16) }); // 16:00
                playTimes.Add(new Spilletid { Day = currentDay.DayOfWeek, StartTime = currentDay.Date.AddHours(21) }); // 21:00
            }

            return playTimes;
        }

        public List<Biograf> GetAllBiografer()
        {
            return Biografer;
        }
    }
}
