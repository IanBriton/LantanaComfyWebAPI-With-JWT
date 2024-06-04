using LantanaComfyAPI.Data;
using LantanaComfyAPI.Interfaces;
using LantanaComfyAPI.Models;

namespace LantanaComfyAPI.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataContext _context;

        public ReservationRepository(DataContext context)
        {
            _context = context;
        }
        public Reservation GetReservation(int reservationId)
        {
            return _context.Reservations.FirstOrDefault(r=>r.Id == reservationId);
        }

        public ICollection<Reservation> GetReservations()
        {
            return _context.Reservations.OrderBy(r=>r.Id).ToList();  
        }

        public bool ReversationExists(int reservationId)
        {
            return _context.Reservations.Any(r => r.Id == reservationId);
        }

        public bool CreateReservation(Reservation reservation)
        {
            _context.Add(reservation);
            return Save();
        }

        public bool DeleteReservation(Reservation reservation)
        {
            _context.Remove(reservation);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
