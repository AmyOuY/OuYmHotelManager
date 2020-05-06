using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDataManager.Library.Models
{
    public class RoomModel
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public string RoomType { get; set; }

        public int RoomCapacity { get; set; }

        public decimal RoomPrice { get; set; }

        public string Description { get; set; }

        public bool IsAvailable { get; set; }
    }
}
