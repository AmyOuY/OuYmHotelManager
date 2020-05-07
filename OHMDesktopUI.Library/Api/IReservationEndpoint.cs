using System.Collections.Generic;
using System.Threading.Tasks;
using OHMDesktopUI.Library.Models;

namespace OHMDesktopUI.Library.Api
{
    public interface IReservationEndpoint
    {
        Task DeleteReservation(int id);
        Task<List<ReservationModel>> GetAllReservations();
        Task<int> GetReservationID(ReservationModel reservation);
        Task PostReservation(ReservationModel reservation);
        Task UpdateReservation(ReservationModel reservation);
    }
}