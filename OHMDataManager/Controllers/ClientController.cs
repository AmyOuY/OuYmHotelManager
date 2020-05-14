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
    public class ClientController : ApiController
    {
        public List<ClientModel> Get()
        {
            ClientData data = new ClientData();

            return data.GetClients();
        }


        [Route("api/Client/PostForID")]
        public int PostForClientID(ClientModel client)
        {
            ClientData data = new ClientData();

            return data.GetClientID(client);
        }


        public void Post(ClientModel client)
        {
            ClientData data = new ClientData();
            data.SaveClient(client);
        }


        public void Put(int id, ClientModel client)
        {
            ClientData data = new ClientData();
            data.UpdateClient(client);
        }


        public void Delete(int id)
        {
            ClientData data = new ClientData();
            data.DeleteClient(id);
        }
    }
}
