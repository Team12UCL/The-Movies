using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
    public Forestilling SelectedForestilling { get; set; }
    public Booking BookingToAdd { get; set; }
    public ICommand AddCommand { get; }

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
        // Initialize the repository via the singleton instance
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
            BookingToAdd.NumberOfTickets != 0)
        {
            return true;
        }
        return false;
    }

    private void AddBooking()
    {
        Booking newBooking = new Booking
        {
            CustomerEmail = BookingToAdd.CustomerEmail,
            CustomerPhone = BookingToAdd.CustomerPhone,
            NumberOfTickets = BookingToAdd.NumberOfTickets,
            BookedForestilling = SelectedForestilling
        };

        _bookingRepository.AddBooking(newBooking);
        Bookinger.Add(newBooking);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
