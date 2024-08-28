using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using The_Movies.Repositories;
using The_Movies.ViewModels;

namespace The_Movies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MovieViewModel movieViewModel;
        ForestillingViewModel forestillingViewModel;
        BookingViewModel bookingViewModel;
        FilmRepository _filmRepository = new FilmRepository();

        public MainWindow()
        {
            InitializeComponent();

            // sæt en viewmodel specifik til BookingTab-tabben
            bookingViewModel = new BookingViewModel();
            BookingTab.DataContext = bookingViewModel;

            // sæt en viewmodel specifik til PlanningTab-tabben
            forestillingViewModel = new ForestillingViewModel();
            PlanningTab.DataContext = forestillingViewModel;

            // sæt en viewmodel specifik til MoviesTab-tabben
            movieViewModel = new MovieViewModel();
            MoviesTab.DataContext = movieViewModel;
        }
    }
}