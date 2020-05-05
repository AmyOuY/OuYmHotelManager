using OHMDataManager.Library.Internal.DataAccess;
using OHMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDataManager.Library.DataAccess
{
    public class ClientData
    {
        public List<ClientModel> GetClients()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<ClientModel, dynamic>("dbo.spClient_GetAll", new { }, "OHMData");

            return output;
        }


        public int GetClientID(ClientModel client)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<int, dynamic>("dbo.spClientIDLookUp", new { client.FirstName, client.LastName, client.Phone }, "OHMData").FirstOrDefault();

            return output;
        }


        public void SaveClient(ClientModel client)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("dbo.spClient_Insert", client, "OHMData");
        }


        public void UpdateClient(ClientModel client)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("dbo.spClient_Update", client, "OHMData");
        }


        public void DeleteClient(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.DeleteData("dbo.spClient_Remove", new { id }, "OHMData");
        }
    }
}
