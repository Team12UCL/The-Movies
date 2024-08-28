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
using The_Movies.Commands;
using The_Movies.Models;
using The_Movies.Repositories;

namespace The_Movies.ViewModels
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private FilmRepository _filmRepository { get; set; }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand ClearCommand { get; }
        public Film MovieToAdd { get; set; }
        private Film _selectedMovie;
        private ObservableCollection<Film> _movies;

        public ObservableCollection<Film> Movies
        {
            get { return _movies; }
            set
            {
                _movies = value;
                OnPropertyChanged(nameof(Movies));
            }
        }

        public Film SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;
                Debug.WriteLine($"Selected movie changed to: {_selectedMovie?.Title ?? "null"}");
                OnPropertyChanged(nameof(SelectedMovie));
            }
        }

        public MovieViewModel()
        {
            _filmRepository = new FilmRepository();
            _filmRepository.LoadMoviesfromCSV();
            Movies = new ObservableCollection<Film>(_filmRepository.GetAllMovies());
            AddCommand = new RelayCommand(x => AddMovie(), x => CanAddMovie());
            RemoveCommand = new RelayCommand(x => RemoveMovie(), x => CanRemoveMovie());
            ClearCommand = new RelayCommand(x => ClearMovie());
            MovieToAdd = new Film();
        }

        private void ClearMovie()
        {
            // Nulstiller værdierne i MovieToAdd
            MovieToAdd.Title = string.Empty;
            //MovieToAdd.Duration = TimeSpan.Zero;
            MovieToAdd.Genre = string.Empty;
            MovieToAdd.Director = string.Empty;
            MovieToAdd.PremiereDate = DateTime.Now;
        }

        private bool CanRemoveMovie()
        {
            // Kan kun trykke "Remove", hvis der er valgt en film
            return SelectedMovie != null;
        }

        private void RemoveMovie()
        {
            if (SelectedMovie != null)
            {
                // Fjerner filmen fra listen over film, fra repository og gemmer ændringerne i CSV-filen
                _filmRepository.RemoveMovie(SelectedMovie);
                Movies.Remove(SelectedMovie);
                _filmRepository.SaveMoviesToCSV(_filmRepository.GetAllMovies());
            }
        }

        private bool CanAddMovie()
        {
            // Kan kun trykke "Add", hvis alle værdier er udfyldt. Det tjekkes i metoden HasAllValues()
            return HasAllValues();
        }

        private bool HasAllValues()
        {
            if (MovieToAdd.Title != null &&
                MovieToAdd.Duration != null &&
                MovieToAdd.Genre != null &&
                MovieToAdd.Director != null)
            {
                return true;
            }
            return false;
        }

        private void AddMovie()
        {
            // Tjekker om filmen allerede er tilføjet
            if (_filmRepository.IsMovieAlreadyAdded(MovieToAdd.Title))
            {
                MessageBox.Show("Filmen med titlen '" + MovieToAdd.Title + "' eksisterer allerede.");
                return;
            }

            // lav et int.TryParse() tjek på MovieToAdd.Duration.TotalMinutes?

            try
            {
                // Lav nyt film objekt med data fra MovieToAdd
                Film newMovie = new Film
                {
                    Title = MovieToAdd.Title,
                    Duration = MovieToAdd.Duration,
                    Genre = MovieToAdd.Genre,
                    Director = MovieToAdd.Director,
                    PremiereDate = MovieToAdd.PremiereDate
                };

                _filmRepository.AddMovie(newMovie);
                Movies.Add(newMovie);
                _filmRepository.SaveMoviesToCSV(_filmRepository.GetAllMovies());
                _filmRepository.LoadMoviesfromCSV();

                MessageBox.Show("Filmen er nu gemt!");
            }
            catch (Exception ex)
            {
                // Error håndtering
                MessageBox.Show("Fejl opstod under tilføjelse af film: " + ex.Message);
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
