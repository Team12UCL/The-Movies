using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using The_Movies.Models;

// Denne klasse er en singleton, hvilket betyder at der kun kan eksistere én instans af klassen ad gangen
// fordi vi ikke har persistens (endnu?) bruges singleton til at sørge for, at alle bruger det samme instans af klassen og dermed den samme data.
// ellers får vi et nyt instans af klassen hver gang vi kalder new FilmRepository() og dermed en ny (tom) liste af film hver gang.

public class ForestillingRepository
{
    private static ForestillingRepository _instance;
    private static readonly object _lock = new object();

    private ObservableCollection<Forestilling> _forestillinger;

    private ForestillingRepository()
    {
        _forestillinger = new ObservableCollection<Forestilling>();

        // testdata: en film
        Film film = new Film
        {
            Title = "Inception",
            Duration = new TimeSpan(2, 28, 0), // 2 hours 28 minutes
            Genre = "Science Fiction",
            Director = "Christopher Nolan",
            PremiereDate = new DateTime(2010, 7, 16)
        };

        // testdata: en forestilling
        _forestillinger.Add(new Forestilling
        {
            Cinema = "CinemaxX",
            Town = "Odense",
            CinemaHall = new Biografsal { Id = "1", Seats = 350 },
            Day = DayOfWeek.Thursday,
            StartTime = new DateTime(2021, 10, 15, 14, 0, 0),
            EndTime = new DateTime(2021, 10, 15, 16, 0, 0),
            Movie = film 
        });
    }

    // For at sikre at der kun er ét instans af ForestillingRepository
    public static ForestillingRepository Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ForestillingRepository();
                }
                return _instance;
            }
        }
    }

    // tilføj en forestilling
    public void AddForestilling(Forestilling forestilling)
    {
        if (forestilling != null)
        {
            _forestillinger.Add(forestilling);
            MessageBox.Show($"Forestillingen er blevet tilføjet til listen. Antal forestillinger: {_forestillinger.Count}");
        }
        else
        {
            MessageBox.Show("Fejl: Forestillingen kunne ikke tilføjes til listen.");
        }
    }

    // hent alle forestillinger
    public ObservableCollection<Forestilling> GetAllForestillinger()
    {
        Debug.WriteLine("Retrieving all forestillinger from repository");
        return _forestillinger;
    }

    // tjek om to forestillinger overlapper i tid
    public bool AreForestillingerOverlapping(Forestilling forestilling, DateTime startTime, DateTime endTime)
    {
        // er der nogen forestillinger i listen, der har samme biograf, by, biografsal og dag --- og overlapper i tid?
        return _forestillinger.Any(f =>
            f.Cinema == forestilling.Cinema &&
            f.Town == forestilling.Town &&
            f.CinemaHall == forestilling.CinemaHall &&
            f.Day == forestilling.Day &&
            (
                (f.StartTime < endTime && f.EndTime > startTime)
            )
        );
    }
}
