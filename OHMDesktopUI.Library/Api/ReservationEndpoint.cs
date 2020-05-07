using OHMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OHMDesktopUI.Library.Api
{
    public class ReservationEndpoint : IReservationEndpoint
    {
        private readonly IAPIHelper _apiHelper;

        public ReservationEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }


        public async Task PostReservation(ReservationModel reservation)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Reservation", reservation))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task<List<ReservationModel>> GetAllReservations()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Reservation"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ReservationModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task<int> GetReservationID(ReservationModel reservation)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Reservation/PostForID", reservation))
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


        public async Task UpdateReservation(ReservationModel reservation)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync($"/api/Reservation/{ reservation.Id }", reservation))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task DeleteReservation(int id)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync($"/api/Reservation/{ id }"))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
