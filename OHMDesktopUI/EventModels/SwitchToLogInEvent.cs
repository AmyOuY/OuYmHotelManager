using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDesktopUI.EventModels
{
    public class SwitchToLogInEvent
    {
        public SwitchToLogInEvent(string client, string phone)
        {
            Client = client;
            Phone = phone;
        }

        public string Client { get; set; }

        public string Phone { get; set; }
    }
}
