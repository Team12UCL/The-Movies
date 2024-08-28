using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using The_Movies.Commands;
using The_Movies.Models;
using The_Movies.Repositories;

namespace The_Movies.ViewModels
{
    public class ForestillingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        BiografRepository _biografRepository { get; set; }
        FilmRepository _filmRepository { get; set; }
        ForestillingRepository _forestillingRepository { get; set; }
        public ObservableCollection<Film> Movies { get; set; }
        public ObservableCollection<Biograf> Cinemas { get; set; }
        public Biograf _selectedCinema { get; set; }
        public Biografsal _selectedCinemaHall { get; set; }
        public Spilletid _selectedPlayTime { get; set; }
        public Film _selectedMovie { get; set; }
        public ICommand AddForestillingCommand { get; set; }
            
        public Film SelectedMovie
        {
            get { return _selectedMovie; }
            set 
            { 
                _selectedMovie = value; 
                Debug.WriteLine("Selected movie changed to: " + _selectedMovie?.Title ?? "null");
                OnPropertyChanged(nameof(SelectedMovie));
            }
        }

        public Spilletid SelectedPlayTime
        {
            get { return _selectedPlayTime; }
            set 
            { 
                _selectedPlayTime = value; 
                Debug.WriteLine("Selected playtime changed to: " + _selectedPlayTime?.StartTime.ToString() ?? "null");
                OnPropertyChanged(nameof(SelectedPlayTime));
            }
        }

        public Biografsal SelectedCinemaHall
        {
            get { return _selectedCinemaHall; }
            set
            {
                _selectedCinemaHall = value;
                Debug.WriteLine("Selected cinema hall changed to: " + _selectedCinemaHall?.Id ?? "null");
                OnPropertyChanged(nameof(SelectedCinemaHall));
            }
        }

        public Biograf SelectedCinema
        {
            get { return _selectedCinema; }
            set
            {
                _selectedCinema = value;
                Debug.WriteLine("Selected cinema changed to: " + _selectedCinema?.CinemaName ?? "null");
                OnPropertyChanged(nameof(SelectedCinema));
            }
        }

        public ForestillingViewModel()
        {
            _biografRepository = new BiografRepository();
            _filmRepository = new FilmRepository();
            _filmRepository.LoadMoviesfromCSV();
            _forestillingRepository = ForestillingRepository.Instance;
            Cinemas = new ObservableCollection<Biograf>(_biografRepository.GetAllBiografer());
            Movies = _filmRepository.GetAllMovies();
            AddForestillingCommand = new RelayCommand(x => AddForestilling(), x => CanAddForestilling());
        }

        public void GetMoviesFromRepo()
        {
            Movies = _filmRepository.GetAllMovies();
        }

        private bool CanAddForestilling()
        {
            // Tjekker om alle værdier er valgt i UI før der kan tilføjes en forestilling
            if (SelectedCinema != null &&
                SelectedCinemaHall != null &&
                SelectedMovie != null &&
                SelectedPlayTime != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AddForestilling()
        {
            DateTime forestillingStartTime = SelectedPlayTime.StartTime;
            TimeSpan movieDuration = SelectedMovie.Duration;
            DateTime calculatedForestillingEndTime = CalculateEndTimeWithCleaningAndCommercials(forestillingStartTime, movieDuration);

            var newForestilling = new Forestilling
            {
                Cinema = SelectedCinema.CinemaName,
                Town = SelectedCinema.Town,
                CinemaHall = SelectedCinemaHall.Id,
                Day = SelectedPlayTime.Day,
                StartTime = SelectedPlayTime.StartTime,
                EndTime = calculatedForestillingEndTime,
                Movie = SelectedMovie
            };

            // Tjek for overlapping forestilling
            if (_forestillingRepository.AreForestillingerOverlapping(newForestilling, forestillingStartTime, calculatedForestillingEndTime))
            {
                MessageBox.Show("Fejl: Forestilling overlapper med en anden forestilling.");
                return;
            }

            _forestillingRepository.AddForestilling(newForestilling);
        }

        private DateTime CalculateEndTimeWithCleaningAndCommercials(DateTime startTime, TimeSpan playTime)
        {
            TimeSpan cleaningAndCommercialsTime = new TimeSpan(0, 30, 0);

            //TimeSpan cleaningTime = new TimeSpan(0, 15, 0);
            //TimeSpan commercialsTime = new TimeSpan(0, 15, 0);
            //TimeSpan timeToAdd = cleaningTime.Add(commercialsTime);

            // Tilføj filmens playtime til tid til rengøring og reklamer til starttidspunktet for at finde sluttidspunktet for forestillingen
            DateTime endTime = startTime.Add(playTime).Add(cleaningAndCommercialsTime);

            return endTime;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
