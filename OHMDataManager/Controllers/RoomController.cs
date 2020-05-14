using OHMDataManager.Library.DataAccess;
using OHMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OHMDataManager.Controllers
{
    [Authorize(Roles = "Manager,Receptionist")]
    public class RoomController : ApiController
    {
        public List<RoomModel> Get()
        {
            RoomData data = new RoomData();

            return data.GetRooms();
        }


        [Route("api/Room/PostForRoom")]
        public RoomModel PostForRoom(RoomModel room)
        {
            RoomData data = new RoomData();

            return data.GetRoom(room);
        }


        [Route("api/Room/PostForID")]
        public int PostForRoomID(RoomModel room)
        {
            RoomData data = new RoomData();

            return data.GetRoomID(room);
        }


        public void Post(RoomModel room)
        {
            RoomData data = new RoomData();
            data.SaveRoom(room);
        }


        public void Put(int id, RoomModel room)
        {
            RoomData data = new RoomData();
            data.UpdateRoom(room);
        }


        public void Delete(int id)
        {
            RoomData data = new RoomData();
            data.DeleteRoom(id);
        }
    }
}
