using LantanaComfyAPI.Models;

namespace LantanaComfyAPI.Interfaces
{
    public interface IReservationRepository
    {
        ICollection<Reservation> GetReservations();
        Reservation GetReservation(int reservationId);
        bool ReversationExists(int reservationId);
        bool CreateReservation(Reservation reservation);
        bool DeleteReservation(Reservation reservation);
        bool Save();
    }
}
