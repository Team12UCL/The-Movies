using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using The_Movies.Models;

namespace The_Movies.Repositories
{
    public class FilmRepository
    {
        private ObservableCollection<Film> _movies;
        private string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FilmListe.csv");
        private string visningFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OversigtVisninger.csv");

        public FilmRepository()
        {
            _movies = new ObservableCollection<Film>();
            //{
            //    //testdata
            //    new Film { Title = "The Shawshank Redemption", Duration = TimeSpan.FromMinutes(142), Genre = "Drama" },
            //    new Film { Title = "The Godfather", Duration = TimeSpan.FromMinutes(175), Genre = "Crime" },
            //    new Film { Title = "The Dark Knight", Duration = TimeSpan.FromMinutes(152), Genre = "Action" },
            //};
            //LoadMoviesfromCSV();
        }

        // metode til at tilføje en film til listen
        public void AddMovie(Film movie)
        {
            if (!IsMovieAlreadyAdded(movie.Title))
            {
                _movies.Add(movie);
                //MessageBox.Show("Filmen er blevet tilføjet til listen.");
            }
            else
            { 
                MessageBox.Show("Fejl: Titlen er allerede tilføjet til listen.");
            }
        }

        // metode til at fjerne en film fra listen
        public void RemoveMovie(Film movie)
        {
            if (movie != null)
            {
                _movies.Remove(movie);
                MessageBox.Show("Filmen er blevet fjernet fra listen.");
            }
            else
            {
                MessageBox.Show("Fejl: Filmen kunne ikke findes i listen.");
            }
        }

        // metode til at hente alle film
        public ObservableCollection<Film> GetAllMovies()
        {
            return _movies;
        }

        // Tjek om en film allerede er tilføjet
        public bool IsMovieAlreadyAdded(string title)
        {
            return _movies.Any(m => m.Title == title);
        }

        // Gem film til en CSV-fil
        public void SaveMoviesToCSV(ObservableCollection<Film> _movies)
        {
            //SaveMoviesAsync();

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                string header = "Title;Duration;Genre";
                sw.WriteLine(header);

                foreach (Film movie in _movies)
                {
                    string movieLine = $"{movie.Title};{movie.Duration.TotalMinutes};{movie.Genre}";
                    sw.WriteLine(movieLine);
                }
            }
        }

        //public async void SaveMoviesAsync()
        //{
        //    try
        //    {
        //        // Asynkron gemme film til CSV for at undgå blockering af UI
        //        await Task.Run(() => _movies);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Fejl opstod under tilføjelse af film til CSV: " + ex.Message);
        //    }
        //}

        // Indlæs film fra en CSV-fil
        public void LoadMoviesfromCSV()
        {
            if (!File.Exists(filePath))
                return;

            using (StreamReader sr = new StreamReader(filePath))
            {
                string headerLine = sr.ReadLine(); // Ignorer header
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] movieLine = line.Split(';');

                    string title = movieLine[0];
                    TimeSpan duration = TimeSpan.FromMinutes(int.Parse(movieLine[1]));
                    string genre = movieLine[2];

                    if (!IsMovieAlreadyAdded(title))
                    {
                        Film newMovie = new Film
                        {
                            Title = title,
                            Duration = duration,
                            Genre = genre
                        };

                        AddMovie(newMovie);
                    }
                }
            }
        }
    }
}
