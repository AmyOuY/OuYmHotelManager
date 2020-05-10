using OHMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OHMDesktopUI.Library.Api
{
    public class CheckOutEndpoint : ICheckOutEndpoint
    {
        private readonly IAPIHelper _apiHelper;

        public CheckOutEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        

        public async Task PostCheckOut(CheckOutModel checkOut)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/CheckOut", checkOut))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
