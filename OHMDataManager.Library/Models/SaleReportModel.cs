using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDataManager.Library.Models
{
    public class SaleReportModel
    {
        public string RoomType { get; set; }

        public int RoomNumber { get; set; }

        public int StayDays { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }
    }
}
