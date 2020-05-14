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
    [Authorize(Roles = "Manager")]
    public class SaleController : ApiController
    {
        [Route("api/Sale/GetSaleReport")]
        public List<SaleReportModel> Get()
        {
            SaleData data = new SaleData();

            return data.GetSaleReport();
        }
    }
}
