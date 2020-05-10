using OHMDataManager.Library.Internal.DataAccess;
using OHMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDataManager.Library.DataAccess
{
    public class CheckInData
    {

        public List<CheckInModel> GetCheckIns()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<CheckInModel, dynamic>("dbo.spCheckIn_GetAll", new { }, "OHMData");

            return output;
        }


        public CheckInModel GetCheckIn(ClientInfo cInfo)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<CheckInModel, dynamic>("dbo.spCheckInLookUp", new { cInfo.Client, cInfo.Phone }, "OHMData").FirstOrDefault();

            return output;
        }


        public int GetCheckInID(CheckInModel checkIn)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<int, dynamic>("dbo.spCheckInIDLookUp", new { checkIn.Client, checkIn.Phone }, "OHMData").FirstOrDefault();

            return output;
        }


        public void SaveCheckIn(CheckInModel checkIn)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("dbo.spCheckIn_Insert", checkIn, "OHMData");
        }


        public void UpdateCheckIn(CheckInModel checkIn)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("dbo.spCheckIn_Update", checkIn, "OHMData");
        }


        public void DeleteCheckIn(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.DeleteData("dbo.spCheckIn_Remove", new { id }, "OHMData");
        }
    }
}
