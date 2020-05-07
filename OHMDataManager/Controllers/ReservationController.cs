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
    [Authorize]
    public class ReservationController : ApiController
    {
        public List<ReservationModel> Get()
        {
            ReservationData data = new ReservationData();

            return data.GetReservations();
        }


        [Route("api/Reservation/PostForID")]
        public int PostForReservationID(ReservationModel reservation)
        {
            ReservationData data = new ReservationData();

            return data.GetReservationID(reservation);
        }


        public void Post(ReservationModel reservation)
        {
            ReservationData data = new ReservationData();
            data.SaveReservation(reservation);
        }


        public void Put(int id, ReservationModel reservation)
        {
            ReservationData data = new ReservationData();
            data.UpdataReservation(reservation);
        }


        public void Delete(int id)
        {
            ReservationData data = new ReservationData();
            data.DeleteReservation(id);
        }
    }
}
