using Caliburn.Micro;
using OHMDesktopUI.Library.Api;
using OHMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OHMDesktopUI.ViewModels
{
    public class ClientViewModel : Screen
    {
        private readonly IClientEndpoint _clientEndpoint;

        public ClientViewModel(IClientEndpoint clientEndpoint)
        {
            _clientEndpoint = clientEndpoint;
        }


        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            await LoadClients();
        }



        private async Task LoadClients()
        {
            var clientList = await _clientEndpoint.GetAllClients();

            Clients = new BindingList<ClientModel>(clientList);
        }


        private BindingList<ClientModel> _clients;

        public BindingList<ClientModel> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                NotifyOfPropertyChange(() => Clients);
            }
        }


        private ClientModel _selectedClient;

        public ClientModel SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                NotifyOfPropertyChange(() => SelectedClient);
                NotifyOfPropertyChange(() => CanEditClient);
                NotifyOfPropertyChange(() => CanRemoveClient);
            }
        }



        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }


        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => CanAddClient);
            }
        }


        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => CanAddClient);
            }
        }


        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
                NotifyOfPropertyChange(() => CanAddClient);
            }
        }


        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                NotifyOfPropertyChange(() => Phone);
                NotifyOfPropertyChange(() => CanAddClient);
            }
        }


        private string _address;

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                NotifyOfPropertyChange(() => Address);
                NotifyOfPropertyChange(() => CanAddClient);
            }
        }


        public bool CanAddClient
        {
            get
            {
                bool output = false;

                if (FirstName?.Length > 0 && LastName?.Length > 0 && Email?.Length > 0 && Phone?.Length > 0 && Address?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task AddClient()
        {
            ClientModel client = new ClientModel
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                Address = Address
            };

            await _clientEndpoint.PostClient(client);

            client.Id = await _clientEndpoint.GetClientID(client);
            Clients.Add(client);
            NotifyOfPropertyChange(() => Clients);

            ResetClientModel();
        }



        public void ResetClientModel()
        {
            Id = 0;
            FirstName = "";
            LastName = "";
            Email = "";
            Phone = "";
            Address = "";
        }


        public bool CanEditClient
        {
            get
            {
                bool output = false;

                if (SelectedClient != null)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task EditClient()
        {
            ClientModel client = new ClientModel
            {
                Id = SelectedClient.Id,
                FirstName = SelectedClient.FirstName,
                LastName = SelectedClient.LastName,
                Email = SelectedClient.Email,
                Phone = SelectedClient.Phone,
                Address = SelectedClient.Address
            };

            await _clientEndpoint.UpdateClient(client);
        }


        public bool CanRemoveClient
        {
            get
            {
                bool output = false;

                if (SelectedClient != null)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task RemoveClient()
        {
            await _clientEndpoint.DeleteClient(SelectedClient.Id);
            Clients.Remove(SelectedClient);
            NotifyOfPropertyChange(() => Clients);
        }


       
        public string SearchBox { get; set; }

        List<ClientModel> filterList = new List<ClientModel>();

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs args)
        {
            //SearchBox = (sender as TextBox).Text;

            filterList.Clear();

            if (SearchBox == "")
            {
                filterList.AddRange(Clients);
            }
            else
            {
                foreach (ClientModel client in Clients)
                {

                    if (client.FirstName.Contains(SearchBox))
                    {
                        filterList.Add(client);
                    }
                }
            }

            Clients = new BindingList<ClientModel>(filterList);
            NotifyOfPropertyChange(() => Clients);
        }

    }
}
