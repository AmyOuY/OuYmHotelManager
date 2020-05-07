using OHMDataManager.Library.Internal.DataAccess;
using OHMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDataManager.Library.DataAccess
{
    public class ReservationData
    {
        public List<ReservationModel> GetReservations()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<ReservationModel, dynamic>("dbo.spReservation_GetAll", new { }, "OHMData");

            return output;
        }


        public int GetReservationID(ReservationModel reservation)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<int, dynamic>("dbo.spReservationIDLookUp", new { reservation.ClientId }, "OHMData").FirstOrDefault();

            return output;
        }


        public void SaveReservation(ReservationModel reservation)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("dbo.spReservation_Insert", reservation, "OHMData");
        }


        public void UpdataReservation(ReservationModel reservation)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("dbo.spReservation_Update", reservation, "OHMData");
        }


        public void DeleteReservation(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.DeleteData("dbo.spReservation_Remove", new { id }, "OHMData");
        }
    }
}
