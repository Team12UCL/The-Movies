using System;
using System.Collections.Generic;
using The_Movies.Models;

namespace The_Movies.Repositories
{
    public class BookingRepository
    {
        private static BookingRepository _instance;  // Singleton instance
        private List<Booking> _bookings;  // Backing field for the list of bookings

        // Public property to access the singleton instance
        public static BookingRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BookingRepository();
                }
                return _instance;
            }
        }

        // Private constructor to prevent direct instantiation
        private BookingRepository()
        {
            _bookings = new List<Booking>();

            _bookings.Add(new Booking
            {
                CustomerEmail = "test@example.com",
                CustomerPhone = "123456789",
                NumberOfTickets = 2,
            });
        }

        // Method to add a new booking
        public void AddBooking(Booking booking)
        {
            _bookings.Add(booking);
        }

        // Method to get all bookings
        public List<Booking> GetAllBookings()
        {
            return _bookings;
        }
    }
}
