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
    public class CheckInViewModel :Screen
    {
        private readonly IRoomEndpoint _roomEndpoint;
        private readonly ICheckInEndpoint _checkInEndpoint;

        public CheckInViewModel(IRoomEndpoint roomEndpoint, ICheckInEndpoint checkInEndpoint)
        {
            _roomEndpoint = roomEndpoint;
            _checkInEndpoint = checkInEndpoint;
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
                NotifyOfPropertyChange(() => CanCheckIn);
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



       
        public DateTime DateIn { get; set; } = DateTime.Now;

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


        private void ClearCheckIn()
        {
            Client = "";
            Phone = "";
            RoomTypes = new BindingList<string>();
            SelectedRoomType = "";
            RoomNumbers = new BindingList<int>();
            SelectedRoomNumber = 0;
            RoomCapacity = 0;
            RoomPrice = 0;
            DateOut = DateTime.Now;
            StayDays = 0;
            GuestNumber = 0;
        }
    }
}
