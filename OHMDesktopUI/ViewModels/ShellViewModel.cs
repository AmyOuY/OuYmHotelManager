using Caliburn.Micro;
using OHMDesktopUI.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private readonly ClientViewModel _clientVM;
        private readonly IEventAggregator _events;

        public ShellViewModel(ClientViewModel clientVM, IEventAggregator events)
        {
            _clientVM = clientVM;
            _events = events;
            _events.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }


        public void Handle(LogOnEvent message)
        {
            ActivateItem(_clientVM);
        }
    }
}
