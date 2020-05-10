using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDesktopUI.Library.Models
{
    public class CheckOutModel
    {
        public int Id { get; set; }

        public int CheckInID { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
