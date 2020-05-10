using Caliburn.Micro;
using OHMDesktopUI.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>, IHandle<SwitchToLogInEvent>
    {
        private readonly ClientViewModel _clientVM;
        private readonly RoomViewModel _roomVM;
        private readonly ReservationViewModel _reservationVM;
        private readonly CheckInViewModel _checkInVM;
        private readonly CheckOutViewModel _checkOutVM;
        private readonly IEventAggregator _events;

        public ShellViewModel(ClientViewModel clientVM, RoomViewModel roomVM, ReservationViewModel reservationVM, 
            CheckInViewModel checkInVM, CheckOutViewModel checkOutVM, IEventAggregator events)
        {
            _clientVM = clientVM;
            _roomVM = roomVM;
            _reservationVM = reservationVM;
            _checkInVM = checkInVM;
            _checkOutVM = checkOutVM;
            _events = events;
            _events.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }


        public void Handle(LogOnEvent message)
        {
            //ActivateItem(_clientVM);
            //ActivateItem(_roomVM);
            //ActivateItem(_reservationVM);
            ActivateItem(_checkOutVM);
        }


        public void Handle(SwitchToLogInEvent message)
        {
            ActivateItem(_checkInVM);
            _checkInVM.Client = message.Client;
            _checkInVM.Phone = message.Phone;
        }
    }
}
