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
    public class ReservationViewModel : Screen
    {
        private readonly IReservationEndpoint _reservationEndpoint;

        public ReservationViewModel(IReservationEndpoint reservationEndpoint)
        {
            _reservationEndpoint = reservationEndpoint;
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
            }
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


        private List<int> _roomNumbers;

        public List<int> RoomNumbers
        {
            get { return _roomNumbers; }
            set
            {
                _roomNumbers = value;
                NotifyOfPropertyChange(() => RoomNumbers);
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
            }
        }


        private DateTime _dateIn;

        public DateTime DateIn
        {
            get { return _dateIn; }
            set
            {
                _dateIn = value;
                NotifyOfPropertyChange(() => DateIn);
            }
        }


        private DateTime _dateOut;

        public DateTime DateOut
        {
            get { return _dateOut; }
            set
            {
                _dateOut = value;
                NotifyOfPropertyChange(() => DateOut);
            }
        }



        public bool CanAddReservation
        {
            get
            {
                bool output = false;

                if (ClientId > 0 && SelectedRoomType?.Length > 0 && SelectedRoomNumber > 0 && DateIn > DateTime.Now && DateTime.Compare(DateIn, DateOut) < 0)
                {
                    output = true;
                }

                return output;
            }
        }


        public void AddReservation()
        {

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


        public void EditReservation()
        {

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


        public void RemoveReservation()
        {

        }


    }
}
