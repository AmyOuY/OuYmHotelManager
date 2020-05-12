using Caliburn.Micro;
using OHMDesktopUI.EventModels;
using OHMDesktopUI.Library.Api;
using OHMDesktopUI.Library.Models;
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
        private readonly CheckInViewModel _checkInVM;
        private readonly CheckOutViewModel _checkOutVM;
        private readonly IEventAggregator _events;
        private readonly BlankViewModel _blankVM;
        private readonly ILoggedInUser _user;
        private readonly IAPIHelper _apiHelper;

        public ShellViewModel(ClientViewModel clientVM, RoomViewModel roomVM, CheckInViewModel checkInVM, CheckOutViewModel checkOutVM,
            IEventAggregator events, BlankViewModel blankVM, ILoggedInUser user, IAPIHelper apiHelper)
        {
            _clientVM = clientVM;
            _roomVM = roomVM;
            _checkInVM = checkInVM;
            _checkOutVM = checkOutVM;
            _events = events;
            _blankVM = blankVM;
            _user = user;
            _apiHelper = apiHelper;
            _events.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }


        public void Handle(LogOnEvent message)
        {
            ActivateItem(_blankVM);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }


        public void Handle(SwitchToLogInEvent message)
        {
            ActivateItem(_checkInVM);
            _checkInVM.Client = message.Client;
            _checkInVM.Phone = message.Phone;
        }


        public bool IsLoggedIn
        {
            get
            {
                bool output = false;

                if (string.IsNullOrWhiteSpace(_user.Token) == false)
                {
                    output = true;
                }

                return output;
            }
        }


        public void ExitApplication()
        {
            TryClose();
        }


        public void LogOut()
        {
            _user.ResetUser();
            _apiHelper.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }


        public void Room()
        {
            ActivateItem(_roomVM);
        }


        public void Client()
        {
            ActivateItem(_clientVM);
        }


        public void CheckIn()
        {
            ActivateItem(_checkInVM);
        }


        public void CheckOut()
        {
            ActivateItem(_checkOutVM);
        }
    }
}
