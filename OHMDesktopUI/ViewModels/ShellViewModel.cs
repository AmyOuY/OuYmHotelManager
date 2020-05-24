using Caliburn.Micro;
using OHMDesktopUI.EventModels;
using OHMDesktopUI.Library.Api;
using OHMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>, IHandle<SwitchToLogInEvent>
    {
        private readonly CheckInViewModel _checkInVM;
        private readonly IEventAggregator _events;
        private readonly ILoggedInUser _user;
        private readonly IAPIHelper _apiHelper;

        public ShellViewModel(CheckInViewModel checkInVM,
                              IEventAggregator events,
                              ILoggedInUser user,
                              IAPIHelper apiHelper)
        {
            _checkInVM = checkInVM;
            _events = events;
            _user = user;
            _apiHelper = apiHelper;
            _events.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }


        public void Handle(LogOnEvent message)
        {
            ActivateItem(IoC.Get<BlankViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
            NotifyOfPropertyChange(() => IsLoggedOut);
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


        public bool IsLoggedOut
        {
            get
            {
                return !IsLoggedIn;
            }
        }


        public void ExitApplication()
        {
            TryClose();
        }


        public void LogIn()
        {
            ActivateItem(IoC.Get<LoginViewModel>());
            
        }


        public void LogOut()
        {
            _user.ResetUser();
            _apiHelper.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
            NotifyOfPropertyChange(() => IsLoggedOut);
        }



        public void UserManagement()
        {
            ActivateItem(IoC.Get<UserDisplayViewModel>());
        }


        public void Room()
        {
            ActivateItem(IoC.Get<RoomViewModel>());
        }


        public void Client()
        {
            ActivateItem(IoC.Get<ClientViewModel>());
        }


        public void CheckIn()
        {
            ActivateItem(IoC.Get<CheckInViewModel>());
        }


        public void CheckOut()
        {
            ActivateItem(IoC.Get<CheckOutViewModel>());
        }
    }
}
