using OHMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OHMDesktopUI.Library.Api
{
    public class RoomEndpoint : IRoomEndpoint
    {
        private readonly IAPIHelper _apiHelper;

        public RoomEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }


        public async Task PostRoom(RoomModel room)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Room", room))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task<List<RoomModel>> GetAllRooms()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Room"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<RoomModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }



        public async Task<RoomModel> GetRoom(RoomModel room)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Room/PostForRoom", room))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<RoomModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task<int> GetRoomId(RoomModel room)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Room/PostForID", room))
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


        public async Task UpdateRoom(RoomModel room)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync($"/api/Room/{ room.Id }", room))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task DeleteRoom(int id)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync($"/api/Room/{ id }"))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
