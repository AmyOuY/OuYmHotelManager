using OHMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OHMDesktopUI.Library.Api
{
    public class CheckInEndpoint : ICheckInEndpoint
    {
        private readonly IAPIHelper _apiHelper;

        public CheckInEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }


        public async Task PostCheckIn(CheckInModel checkIn)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/CheckIn", checkIn))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }



        public async Task<List<CheckInModel>> GetAllCheckIns()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/CheckIn"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<CheckInModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task<CheckInModel> GetCheckIn(ClientInfo cInfo)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/CheckIn/PostForCheckIn", cInfo))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<CheckInModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }



        public async Task<int> GetCheckInID(CheckInModel checkIn)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/CheckIn/PostForID", checkIn))
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


        public async Task UpdateCheckIn(CheckInModel checkIn)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync($"/api/CheckIn/{ checkIn.Id }", checkIn))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task DeleteCheckIn(int id)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync($"/api/CheckIn/{ id }"))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
