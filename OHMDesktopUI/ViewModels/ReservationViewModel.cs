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
    public class ReservationViewModel : Screen
    {
        private readonly IReservationEndpoint _reservationEndpoint;
        private readonly IRoomEndpoint _roomEndpoint;

        public ReservationViewModel(IReservationEndpoint reservationEndpoint, IRoomEndpoint roomEndpoint)
        {
            _reservationEndpoint = reservationEndpoint;
            _roomEndpoint = roomEndpoint;
        }



        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadReservations();
        }



        private async Task LoadReservations()
        {
            var reservationList = await _reservationEndpoint.GetAllReservations();
            Reservations = new BindingList<ReservationModel>(reservationList);
        }



        private BindingList<ReservationModel> _reservations;

        public BindingList<ReservationModel> Reservations
        {
            get { return _reservations; }
            set
            {
                _reservations = value;
                NotifyOfPropertyChange(() => Reservations);
            }
        }


        private ReservationModel _selectedReservation;

        public ReservationModel SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                _selectedReservation = value;
                NotifyOfPropertyChange(() => SelectedReservation);
                NotifyOfPropertyChange(() => CanEditReservation);
                NotifyOfPropertyChange(() => CanRemoveReservation);
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


        private int _clientId;

        public int ClientId
        {
            get { return _clientId; }
            set
            {
                _clientId = value;
                NotifyOfPropertyChange(() => ClientId);
                NotifyOfPropertyChange(() => CanAddReservation);
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
                NotifyOfPropertyChange(() => CanAddReservation);
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
                NotifyOfPropertyChange(() => CanAddReservation);
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
                NotifyOfPropertyChange(() => CanAddReservation);

            }
        }


       


        private DateTime _dateOut = DateTime.Now;

        public DateTime DateOut
        {
            get { return _dateOut; }
            set
            {
                _dateOut = value;
                NotifyOfPropertyChange(() => DateOut);
                NotifyOfPropertyChange(() => CanAddReservation);
            }
        }

        

        public bool CanAddReservation
        {
            get
            {
                bool output = false;
                
                if (ClientId > 0 && SelectedRoomType?.Length > 0 && SelectedRoomNumber > 0 && DateTime.Compare(DateIn, DateTime.Now) > 0 && DateTime.Compare(DateIn, DateOut) < 0)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task AddReservation()
        {
            ReservationModel reservation = new ReservationModel
            {
                ClientId = ClientId,
                RoomType = SelectedRoomType,
                RoomNumber = SelectedRoomNumber,
                DateIn = DateIn,
                DateOut = DateOut
            };

            await _reservationEndpoint.PostReservation(reservation);

            reservation.Id = await _reservationEndpoint.GetReservationID(reservation);

            Reservations.Add(reservation);
            NotifyOfPropertyChange(() => Reservations);

            ResetReservation();
        }


        private void ResetReservation()
        {
            ClientId = 0;
            DateIn = DateTime.Now;
            DateOut = DateTime.Now;
        }


        public bool CanEditReservation
        {
            get
            {
                bool output = false;

                if (SelectedReservation != null)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task EditReservation()
        {
            ReservationModel reservation = new ReservationModel
            {
                Id = SelectedReservation.Id,
                ClientId = SelectedReservation.ClientId,
                RoomType = SelectedReservation.RoomType,
                RoomNumber = SelectedReservation.RoomNumber,
                DateIn = SelectedReservation.DateIn,
                DateOut = SelectedReservation.DateOut
            };

            await _reservationEndpoint.UpdateReservation(reservation);
        }



        public bool CanRemoveReservation
        {
            get
            {
                bool output = false;

                if (SelectedReservation != null)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task RemoveReservation()
        {
            await _reservationEndpoint.DeleteReservation(SelectedReservation.Id);
            Reservations.Remove(SelectedReservation);
            NotifyOfPropertyChange(() => Reservations);
        }


    }
}
