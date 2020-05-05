using System.Collections.Generic;
using System.Threading.Tasks;
using OHMDesktopUI.Library.Models;

namespace OHMDesktopUI.Library.Api
{
    public interface IClientEndpoint
    {
        Task PostClient(ClientModel client);

        Task<List<ClientModel>> GetAllClients();

        Task<int> GetClientID(ClientModel client);

        Task UpdateClient(ClientModel client);

        Task DeleteClient(int id);
    }
}