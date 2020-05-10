using System.Collections.Generic;
using System.Threading.Tasks;
using OHMDesktopUI.Library.Models;

namespace OHMDesktopUI.Library.Api
{
    public interface IRoomEndpoint
    {       
        Task<List<RoomModel>> GetAllRooms();
        Task<int> GetRoomId(RoomModel room);
        Task PostRoom(RoomModel room);
        Task UpdateRoom(RoomModel room);
        Task DeleteRoom(int id);
        Task<RoomModel> GetRoom(RoomModel room);
    }
}