using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDataManager.Library.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string RoomType { get; set; }

        public int RoomNumber { get; set; }

        public DateTime DateIn { get; set; }

        public DateTime DateOut { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
