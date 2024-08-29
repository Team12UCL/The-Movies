using System;
using System.Collections.Generic;
using The_Movies.Models;

namespace The_Movies.Repositories
{
    public class BookingRepository
    {
        private static BookingRepository _instance;  // Singleton instans
        private List<Booking> _bookings;

        // For at sikre at der kun er ét instans af BookingRepository
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
        private BookingRepository()
        {
            _bookings = new List<Booking>();

            // testdata: en booking
            _bookings.Add(new Booking
            {
                CustomerEmail = "test@example.com",
                CustomerPhone = "123456789",
                NumberOfTickets = 2,
            });
        }

        // tilføj en booking
        public void AddBooking(Booking booking)
        {
            _bookings.Add(booking);
        }

        // hent alle bookinger
        public List<Booking> GetAllBookings()
        {
            return _bookings;
        }
    }
}
