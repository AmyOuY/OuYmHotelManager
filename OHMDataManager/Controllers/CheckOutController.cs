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
    [Authorize]
    public class CheckOutController : ApiController
    {
        public void Post(CheckOutModel checkOut)
        {
            CheckOutData data = new CheckOutData();

            data.SaveCheckOut(checkOut);
        }
    }
}
