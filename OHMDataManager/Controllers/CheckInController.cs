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
    [Authorize(Roles = "Receptionist")]
    public class CheckInController : ApiController
    {
        public List<CheckInModel> Get()
        {
            CheckInData data = new CheckInData();

            return data.GetCheckIns();
        }


        [Route("api/CheckIn/PostForCheckIn")]
        public CheckInModel PostForCheckIn(ClientInfo cInfo)
        {
            CheckInData data = new CheckInData();

            return data.GetCheckIn(cInfo);
        }



        [Route("api/CheckIn/PostForID")]
        public int PostForCheckInID(CheckInModel checkIn)
        {
            CheckInData data = new CheckInData();

            return data.GetCheckInID(checkIn);
        }


        public void Post(CheckInModel checkIn)
        {
            CheckInData data = new CheckInData();
            data.SaveCheckIn(checkIn);
        }


        public void Put(int id, CheckInModel checkIn)
        {
            CheckInData data = new CheckInData();
            data.UpdateCheckIn(checkIn);
        }


        public void Delete(int id)
        {
            CheckInData data = new CheckInData();
            data.DeleteCheckIn(id);
        }
    }
}
