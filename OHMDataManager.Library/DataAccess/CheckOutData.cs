using OHMDataManager.Library.Internal.DataAccess;
using OHMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDataManager.Library.DataAccess
{
    public class CheckOutData
    {
        public void SaveCheckOut(CheckOutModel checkOut)
        {
            SqlDataAccess data = new SqlDataAccess();

            data.SaveData("dbo.spCheckOut_Insert", checkOut, "OHMData");
        }
    }
}
