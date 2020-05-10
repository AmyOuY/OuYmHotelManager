using OHMDataManager.Library.Internal.DataAccess;
using OHMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDataManager.Library.DataAccess
{
    public class RoomData
    {
        public List<RoomModel> GetRooms()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<RoomModel, dynamic>("dbo.spRoom_GetAll", new { }, "OHMData");

            return output;
        }



        public RoomModel GetRoom(RoomModel room)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<RoomModel, dynamic>("dbo.spRoomLookUp", new { room.RoomNumber }, "OHMData").FirstOrDefault();

            return output;
        }


        public int GetRoomID(RoomModel room)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<int, dynamic>("dbo.spRoomIDLookUp", new { room.RoomNumber }, "OHMData").FirstOrDefault();

            return output;
        }


        public void SaveRoom(RoomModel room)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("dbo.spRoom_Insert", room, "OHMData");
        }


        public void UpdateRoom(RoomModel room)
        {
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spRoom_Update", room, "OHMData");
        }


        public void DeleteRoom(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.DeleteData("dbo.spRoom_Remove", new { id }, "OHMData");
        }
    }
}
