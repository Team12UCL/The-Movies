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

    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
