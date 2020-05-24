using Caliburn.Micro;
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
    public class CheckInViewModel : Screen
    {
        private readonly IRoomEndpoint _roomEndpoint;
        private readonly ICheckInEndpoint _checkInEndpoint;
        private readonly IClientEndpoint _clientEndpoint;

        public CheckInViewModel(IRoomEndpoint roomEndpoint, ICheckInEndpoint checkInEndpoint, IClientEndpoint clientEndpoint)
        {
            _roomEndpoint = roomEndpoint;
            _checkInEndpoint = checkInEndpoint;
            _clientEndpoint = clientEndpoint;
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


        private string _client;

        public string Client
        {
            get { return _client; }
            set
            {   
                _client = value;
                NotifyOfPropertyChange(() => Client);

                FillCheckedIn();

                NotifyOfPropertyChange(() => CanCheckIn);
                NotifyOfPropertyChange(() => CanRemoveCheckIn);
            }
        }


        public List<CheckInModel> AllCheckIns { get; set; }
        public async Task GetAllCheckIns()
        {
            AllCheckIns = await _checkInEndpoint.GetAllCheckIns();
        }


        public async Task FillCheckedIn()
        {
            bool clientRegistered = false;
            string[] names = Client.Split(null);
            ErrorMessage = "";

            List<ClientModel> allClients = await _clientEndpoint.GetAllClients();

            foreach (ClientModel client in allClients)
            {
                if (client.FirstName == names[0] && client.LastName == names[1])
                {
                    clientRegistered = true;
                    Phone = client.Phone;
                }
            }

            if (Client?.Length > 0 && clientRegistered == false)
            {
                ErrorMessage = "Error! You need to register client first!";
                return;
            }

            List<CheckInModel> allCheckedIns = await _checkInEndpoint.GetAllCheckIns();
            allCheckedIns = allCheckedIns.OrderByDescending(x => x.CreatedDate).ToList();

            CheckInModel checkedIn = allCheckedIns.Where(x => x.Client == Client).FirstOrDefault();

            if (checkedIn != null && checkedIn.IsCheckedOut == false)
            {
                Client = checkedIn.Client;
                Id = checkedIn.Id;
                Phone = checkedIn.Phone;
                SelectedRoomType = checkedIn.RoomType;
                SelectedRoomNumber = checkedIn.RoomNumber;
                RoomTypes = new BindingList<string> { checkedIn.RoomType };
                RoomNumbers = new BindingList<int> { checkedIn.RoomNumber };
                RoomCapacity = checkedIn.RoomCapacity;
                RoomPrice = checkedIn.RoomPrice;
                DateIn = checkedIn.DateIn;
                DateOut = checkedIn.DateOut;
                StayDays = checkedIn.StayDays;
                GuestNumber = checkedIn.GuestNumber;
            }
            else
            {
                Id = 0;
                RoomTypes = new BindingList<string> { "single", "double", "triple", "quad" };
                RoomNumbers = new BindingList<int>();
                RoomCapacity = 0;
                RoomPrice = 0;
                DateIn = DateTime.Now;
                DateOut = DateTime.Now;
                StayDays = 0;
                GuestNumber = 0;
            }
        }


        public bool IsErrorVisible
        {
            get
            {
                bool output = false;
                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }



        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
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
                NotifyOfPropertyChange(() => CanCheckIn);
            }
        }
       

        private BindingList<string> _roomTypes = new BindingList<string> { "single", "double", "triple", "quad" };

        public BindingList<string> RoomTypes
        {
            get { return _roomTypes; }
            set
            {
                _roomTypes = value;
                NotifyOfPropertyChange(() => RoomTypes);
            }
        }


        private string _selectedRoomType;

        public string SelectedRoomType
        {
            get { return _selectedRoomType; }
            set
            {
                _selectedRoomType = value;
                NotifyOfPropertyChange(() => SelectedRoomType);
                NotifyOfPropertyChange(() => CanCheckIn);
                RoomNumbers = new BindingList<int>();
                LoadAvailableRooms();
            }
        }


        private BindingList<int> _roomNumbers = new BindingList<int>();

        public BindingList<int> RoomNumbers
        {
            get { return _roomNumbers; }
            set
            {
                _roomNumbers = value;
                NotifyOfPropertyChange(() => RoomNumbers);
            }
        }



        private async Task LoadAvailableRooms()
        {
            List<RoomModel> allRooms = await _roomEndpoint.GetAllRooms();

            foreach (RoomModel room in allRooms)
            {
                if (room.IsAvailable && room.RoomType == SelectedRoomType)
                {
                    RoomNumbers.Add(room.RoomNumber);
                }
            }
        }



        private int _selectedRoomNumber;

        public int SelectedRoomNumber
        {
            get { return _selectedRoomNumber; }
            set
            {
                _selectedRoomNumber = value;
                NotifyOfPropertyChange(() => SelectedRoomNumber);
                GetRoomInfo();
                NotifyOfPropertyChange(() => RoomCapacity);
                NotifyOfPropertyChange(() => RoomPrice);
                NotifyOfPropertyChange(() => CanCheckIn);

            }
        }



        private async Task GetRoomInfo()
        {
            List<RoomModel> allRooms = await _roomEndpoint.GetAllRooms();
            RoomModel selectedRoom = allRooms.Where(x => x.RoomNumber == SelectedRoomNumber).FirstOrDefault();
            RoomPrice = selectedRoom.RoomPrice;
            RoomCapacity = selectedRoom.RoomCapacity;
        }


        private int _roomCapacity;

        public int RoomCapacity
        {
            get { return _roomCapacity; }
            set
            {
                _roomCapacity = value;
                NotifyOfPropertyChange(() => RoomCapacity);
                NotifyOfPropertyChange(() => CanCheckIn);
            }
        }



        private decimal _roomPrice;

        public decimal RoomPrice
        {
            get { return _roomPrice; }
            set
            {
                _roomPrice = value;
                NotifyOfPropertyChange(() => RoomPrice);
                NotifyOfPropertyChange(() => CanCheckIn);
            }
        }



        private DateTime _dateIn = DateTime.Now;

        public DateTime DateIn
        {
            get { return _dateIn; }
            set
            {
                _dateIn = value;
                NotifyOfPropertyChange(() => DateIn);
            }
        }



        private DateTime _dateOut = DateTime.Now;

        public DateTime DateOut
        {
            get { return _dateOut; }
            set
            {
                _dateOut = value;
                StayDays = (DateOut - DateIn).Days + 1;
                NotifyOfPropertyChange(() => DateOut);
                NotifyOfPropertyChange(() => StayDays);
                NotifyOfPropertyChange(() => CanCheckIn);
            }
        }


        private int _stayDays;

        public int StayDays
        {
            get { return _stayDays; }
            set
            {
                _stayDays = value;
                NotifyOfPropertyChange(() => StayDays);
                NotifyOfPropertyChange(() => CanCheckIn);
            }
        }



        private int _guestNumber;

        public int GuestNumber
        {
            get { return _guestNumber; }
            set
            {
                _guestNumber = value;
                NotifyOfPropertyChange(() => GuestNumber);
                NotifyOfPropertyChange(() => CanCheckIn);
            }
        }


        public bool CanCheckIn
        {
            get
            {
                bool output = false;

                if (Client?.Length > 0 && Phone?.Length > 0 && SelectedRoomType?.Length > 0 && SelectedRoomNumber > 0
                    && RoomCapacity > 0 && RoomPrice > 0 && DateTime.Compare(DateIn, DateOut) < 0 && StayDays > 0 && GuestNumber > 0)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task CheckIn()
        {
            CheckInModel checkIn = new CheckInModel
            {
                Client = Client,
                Phone = Phone,
                RoomType = SelectedRoomType,
                RoomNumber = SelectedRoomNumber,
                RoomCapacity = RoomCapacity,
                RoomPrice = RoomPrice,
                DateIn = DateIn,
                DateOut = DateOut,
                StayDays = StayDays,
                GuestNumber = GuestNumber
            };

            await _checkInEndpoint.PostCheckIn(checkIn);

            List<RoomModel> allRooms = await _roomEndpoint.GetAllRooms();

            RoomModel checkedInRoom = allRooms.Where(x => x.RoomNumber == SelectedRoomNumber).FirstOrDefault();
            checkedInRoom.IsAvailable = false;
            await _roomEndpoint.UpdateRoom(checkedInRoom);

            ClearCheckIn();
        }



        public void ClearCheckIn()
        {
            ErrorMessage = "";
            Id = 0;
            Client = "";
            Phone = "";
            RoomTypes = new BindingList<string> { "single", "double", "triple", "quad" };
            RoomNumbers = new BindingList<int>();
            RoomCapacity = 0;
            RoomPrice = 0;
            DateIn = DateTime.Now;
            DateOut = DateTime.Now;
            StayDays = 0;
            GuestNumber = 0;
        }


        public bool CanRemoveCheckIn
        {
            get
            {
                bool output = false;

                if (Id > 0)
                {
                    output = true;
                }

                return output;
            }
        }



        public async Task RemoveCheckIn()
        {

            RoomModel roomInfo = new RoomModel
            {
                RoomNumber = SelectedRoomNumber
            };

            RoomModel room = await _roomEndpoint.GetRoom(roomInfo);

            room.IsAvailable = true;

            await _roomEndpoint.UpdateRoom(room);

            await _checkInEndpoint.DeleteCheckIn(Id);

            ClearCheckIn();
        }
    }
}
