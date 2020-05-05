using OHMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OHMDesktopUI.Library.Api
{
    public class ClientEndpoint : IClientEndpoint
    {
        private readonly IAPIHelper _apiHelper;

        public ClientEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }


        public async Task PostClient(ClientModel client)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Client", client))
            {
                if (response.IsSuccessStatusCode == false)
                {                 
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task<List<ClientModel>> GetAllClients()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Client"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ClientModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

   
        public async Task<int> GetClientID(ClientModel client)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Client/PostForID", client))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<int>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task UpdateClient(ClientModel client)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync($"/api/Client/{ client.Id }", client))
            {
                if (response.IsSuccessStatusCode == false)
                {                  
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task DeleteClient(int id)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync($"/api/Client/{ id }"))
            {
                if (response.IsSuccessStatusCode == false)
                {                  
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
