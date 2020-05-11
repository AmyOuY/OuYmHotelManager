using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDataManager.Library.Models
{
    public class CheckInModel
    {
        public int Id { get; set; }

        public string Client { get; set; }

        public string Phone { get; set; }

        public string RoomType { get; set; }

        public int RoomNumber { get; set; }

        public int RoomCapacity { get; set; }

        public decimal RoomPrice { get; set; }

        public DateTime DateIn { get; set; }

        public DateTime DateOut { get; set; }

        public int StayDays { get; set; }

        public int GuestNumber { get; set; }

        public bool IsCheckedOut { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
