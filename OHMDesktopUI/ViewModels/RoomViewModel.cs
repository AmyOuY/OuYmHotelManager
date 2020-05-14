using Caliburn.Micro;
using OHMDesktopUI.Library.Api;
using OHMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OHMDesktopUI.ViewModels
{
    public class RoomViewModel : Screen
    {
        private readonly IRoomEndpoint _roomEndpoint;
        private readonly StatusInfoViewModel _status;
        private readonly IWindowManager _window;

        public RoomViewModel(IRoomEndpoint roomEndpoint, StatusInfoViewModel status, IWindowManager window)
        {
            _roomEndpoint = roomEndpoint;
            _status = status;
            _window = window;
        }


        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            try
            {
                await LoadRooms();
            }
            catch (Exception ex)
            {

                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";
                if (ex.Message == "Unauthorized")
                {
                    _status.UpdateMessage("Unauthorized Access", "You don't have permission to interact with the Room form.");
                    _window.ShowDialog(_status, null, settings);
                }
                else
                {
                    _status.UpdateMessage("Fatal Exception", ex.Message);
                    _window.ShowDialog(_status, null, settings);
                }

                TryClose();
            }
        }

        
        private async Task LoadRooms()
        {
            var roomList = await _roomEndpoint.GetAllRooms();

            Rooms = new BindingList<RoomModel>(roomList);
        }


        private BindingList<RoomModel> _rooms;

        public BindingList<RoomModel> Rooms
        {
            get { return _rooms; }
            set
            {
                _rooms = value;
                NotifyOfPropertyChange(() => Rooms);
            }
        }


        private RoomModel _selectedRoom;

        public RoomModel SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                _selectedRoom = value;
                NotifyOfPropertyChange(() => SelectedRoom);
                NotifyOfPropertyChange(() => CanEditRoom);
                NotifyOfPropertyChange(() => CanRemoveRoom);
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
                NotifyOfPropertyChange(() => CanAddRoom);                
            }
        }



        private int _roomNumber;

        public int RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                _roomNumber = value;
                NotifyOfPropertyChange(() => RoomNumber);
                NotifyOfPropertyChange(() => CanAddRoom);
            }
        }


        private BindingList<string> _roomTypes = new BindingList<string> { "single", "double", "triple", "quad"};

        public BindingList<string> RoomTypes
        {
            get { return _roomTypes; }
            set
            {
                _roomTypes = value;
                NotifyOfPropertyChange(() => RoomTypes);
                NotifyOfPropertyChange(() => CanAddRoom);
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
            }
        }


        private BindingList<int> _roomCapacities = new BindingList<int> { 1, 2, 3, 4, 5, 6};

        public BindingList<int> RoomCapacities
        {
            get { return _roomCapacities; }
            set
            {
                _roomCapacities = value;
                NotifyOfPropertyChange(() => RoomCapacities);
                NotifyOfPropertyChange(() => CanAddRoom);
            }
        }


        private int _selectedRoomCapacity;

        public int SelectedRoomCapacity
        {
            get { return _selectedRoomCapacity; }
            set
            {
                _selectedRoomCapacity = value;
                NotifyOfPropertyChange(() => SelectedRoomCapacity);
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
                NotifyOfPropertyChange(() => CanAddRoom);
            }
        }


        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => Description);
                NotifyOfPropertyChange(() => CanAddRoom);
            }
        }



        private BindingList<bool> _isAvailables = new BindingList<bool> { true, false };

        public BindingList<bool> IsAvailables
        {
            get { return _isAvailables; }
            set
            {
                _isAvailables = value;
                NotifyOfPropertyChange(() => IsAvailables);
                NotifyOfPropertyChange(() => CanAddRoom);
            }
        }


        private bool _selectedIsAvailable;

        public bool SelectedIsAvailable
        {
            get { return _selectedIsAvailable; }
            set
            {
                _selectedIsAvailable = value;
                NotifyOfPropertyChange(() => SelectedIsAvailable);
            }
        }



        private bool IsRoomNumberExisting()
        {
            foreach (RoomModel room in Rooms)
            {
                if (room.RoomNumber == RoomNumber)
                {
                    return true;
                }
            }

            return false;
        }


        public bool CanAddRoom
        {
            get
            {
                bool output = false;

                if (RoomNumber > 0 && !IsRoomNumberExisting() && SelectedRoomType?.Length > 0 && SelectedRoomCapacity > 0 && RoomPrice > 0 && Description?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task AddRoom()
        {
            RoomModel room = new RoomModel
            {
                RoomNumber = RoomNumber,
                RoomType = SelectedRoomType,
                RoomCapacity = SelectedRoomCapacity,
                RoomPrice = RoomPrice,
                Description = Description,
                IsAvailable = SelectedIsAvailable
            };

            await _roomEndpoint.PostRoom(room);

            room.Id = await _roomEndpoint.GetRoomId(room);

            Rooms.Add(room);
            NotifyOfPropertyChange(() => Rooms);

            ResetRoomModel();
        }


        public void ResetRoomModel()
        {
            RoomNumber = 0;
            SelectedRoomType = "";
            SelectedRoomCapacity = 0;
            RoomPrice = 0;
            SelectedIsAvailable = false;
            Description = "";           
        }


        public bool CanEditRoom
        {
            get
            {
                bool output = false;

                if (SelectedRoom != null)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task EditRoom()
        {
            RoomModel room = new RoomModel
            {
                Id = SelectedRoom.Id,
                RoomNumber = SelectedRoom.RoomNumber,
                RoomType = SelectedRoom.RoomType,
                RoomCapacity = SelectedRoom.RoomCapacity,
                RoomPrice = SelectedRoom.RoomPrice,
                Description = SelectedRoom.Description,
                IsAvailable = SelectedRoom.IsAvailable
            };

            await _roomEndpoint.UpdateRoom(room);

            SelectedRoom = null;
        }


        public bool CanRemoveRoom
        {
            get
            {
                bool output = false;

                if (SelectedRoom != null)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task RemoveRoom()
        {
            await _roomEndpoint.DeleteRoom(SelectedRoom.Id);
            Rooms.Remove(SelectedRoom);
            NotifyOfPropertyChange(() => Rooms);

            SelectedRoom = null;
        }

    }
}
