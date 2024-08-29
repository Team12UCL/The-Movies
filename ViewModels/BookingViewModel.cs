using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using The_Movies.Commands;
using The_Movies.Models;
using The_Movies.Repositories;

public class BookingViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private ForestillingRepository _forestillingRepository { get; set; }
    private BookingRepository _bookingRepository { get; set; }
    private ObservableCollection<Forestilling> _forestillinger { get; set; }
    public ObservableCollection<Booking> Bookinger { get; set; }
    public Forestilling _selectedForestilling { get; set; }
    public Booking BookingToAdd { get; set; }
    public ICommand AddCommand { get; }
    private int _numberOfSeats;

    public Forestilling SelectedForestilling
    {
        get { return _selectedForestilling; }
        set
        {
            _selectedForestilling = value;
            OnPropertyChanged(nameof(SelectedForestilling));
            // opdater antal sæder tilbage i biografsalen når en forestilling vælges (altså, ud fra den valgte biografsal)
            NumberOfSeats = _selectedForestilling.CinemaHall.Seats;
            Debug.WriteLine($"SelectedForestilling remaining seats: {_numberOfSeats}");
        }
    }

    public int NumberOfSeats
    {
        get { return _numberOfSeats; }
        set
        {
            if (_numberOfSeats != value)
            {
                _numberOfSeats = value;
                OnPropertyChanged(nameof(NumberOfSeats));
                Debug.WriteLine($"Updated NumberOfSeats: {_numberOfSeats}");
            }
        }
    }

    public ObservableCollection<Forestilling> Forestillinger
    {
        get => _forestillinger;
        set
        {
            _forestillinger = value;
            OnPropertyChanged(nameof(Forestillinger));
        }
    }

    public BookingViewModel()
    {
        _forestillingRepository = ForestillingRepository.Instance;
        _bookingRepository = BookingRepository.Instance;
        Forestillinger = _forestillingRepository.GetAllForestillinger();
        Bookinger = new ObservableCollection<Booking>(_bookingRepository.GetAllBookings());
        BookingToAdd = new Booking();
        AddCommand = new RelayCommand(x => AddBooking(), x => CanAddBooking());
    }

    private bool CanAddBooking()
    {
        if ((BookingToAdd.CustomerEmail != null ||
            BookingToAdd.CustomerPhone != null) &&
            BookingToAdd.NumberOfTickets != 0 &&
            SelectedForestilling != null)
        {
            return true;
        }
        return false;
    }

    // tilføj en booking ud fra input i UI
    public void AddBooking()
    {
        Booking newBooking = new Booking
        {
            CustomerEmail = BookingToAdd.CustomerEmail,
            CustomerPhone = BookingToAdd.CustomerPhone,
            NumberOfTickets = BookingToAdd.NumberOfTickets,
            BookedForestilling = SelectedForestilling
        };

        if (BookingToAdd.NumberOfTickets > NumberOfSeats)
        {
            MessageBox.Show("Ikke nok sæder tilgængelige til forestillingen");
            return;
        }
        else
        {
            NumberOfSeats -= BookingToAdd.NumberOfTickets;
        }

        _bookingRepository.AddBooking(newBooking);
        Bookinger.Add(newBooking);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
