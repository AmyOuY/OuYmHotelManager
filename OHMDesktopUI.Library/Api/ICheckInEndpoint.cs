using System.Collections.Generic;
using System.Threading.Tasks;
using OHMDesktopUI.Library.Models;

namespace OHMDesktopUI.Library.Api
{
    public interface ICheckInEndpoint
    {
        Task DeleteCheckIn(int id);
        Task<int> GetCheckInID(CheckInModel checkIn);
        Task PostCheckIn(CheckInModel checkIn);
        Task<List<CheckInModel>> GetAllCheckIns();
        Task UpdateCheckIn(CheckInModel checkIn);
        Task<CheckInModel> GetCheckIn(ClientInfo cInfo);
    }
}