using OHMDataManager.Library.Internal.DataAccess;
using OHMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDataManager.Library.DataAccess
{
    public class UserData
    {
        public UserModel GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookUp", new { Id }, "OHMData").FirstOrDefault();

            return output;
        }
    }
}
