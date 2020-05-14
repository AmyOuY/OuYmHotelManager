using Caliburn.Micro;
using OHMDesktopUI.Library.Api;
using OHMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHMDesktopUI.ViewModels
{
    public class CheckOutViewModel : Screen
    {
        private readonly ICheckOutEndpoint _checkOutEndpoint;
        private readonly ICheckInEndpoint _checkInEndpoint;
        private readonly IRoomEndpoint _roomEndpoint;

        public CheckOutViewModel(ICheckOutEndpoint checkOutEndpoint, ICheckInEndpoint checkInEndpoint, IRoomEndpoint roomEndpoint)
        {
            _checkOutEndpoint = checkOutEndpoint;
            _checkInEndpoint = checkInEndpoint;
            _roomEndpoint = roomEndpoint;
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
            }
        }



        private string _roomType;

        public string RoomType
        {
            get { return _roomType; }
            set
            {
                _roomType = value;
                NotifyOfPropertyChange(() => RoomType);
            }
        }



        private int _roomNumber;

        public int RoomNumber
        {
            get { return _roomNumber; }
            set {
                _roomNumber = value;
                NotifyOfPropertyChange(() => RoomNumber);
                GetCheckedInInfo();

                NotifyOfPropertyChange(() => SubTotal);
                NotifyOfPropertyChange(() => Tax);
                NotifyOfPropertyChange(() => Total);
                NotifyOfPropertyChange(() => CanCheckOut);
            }
        }


        public CheckInModel CheckedIn { get; set; }


        public async Task GetCheckedInInfo()
        {
            List<CheckInModel> allCheckIns = await _checkInEndpoint.GetAllCheckIns();
            allCheckIns = allCheckIns.OrderByDescending(x => x.CreatedDate).ToList();

            CheckedIn = allCheckIns.Where(x => x.RoomNumber == RoomNumber).FirstOrDefault();

            ErrorMessage = "";

            if (RoomNumber > 0)
            {
                if (CheckedIn == null)
                {
                    ErrorMessage = "Error! Room not checked in!";
                    return;
                }
                else if (CheckedIn.IsCheckedOut == true)
                {
                    ErrorMessage = "Error! Room already checked out!";
                    return;
                }
                else
                {
                    Client = CheckedIn.Client;
                    Phone = CheckedIn.Phone;
                    RoomType = CheckedIn.RoomType;
                    RoomNumber = CheckedIn.RoomNumber;
                    RoomCapacity = CheckedIn.RoomCapacity;
                    RoomPrice = CheckedIn.RoomPrice;
                    DateIn = CheckedIn.DateIn;
                    DateOut = CheckedIn.DateOut;
                    StayDays = CheckedIn.StayDays;
                    GuestNumber = CheckedIn.GuestNumber;
                }
            }
            else if (RoomNumber < 0)
            {
                ErrorMessage = "Error! Room number does not exist!";
                return;
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



        private int _roomCapacity;

        public int RoomCapacity
        {
            get { return _roomCapacity; }
            set
            {
                _roomCapacity = value;
                NotifyOfPropertyChange(() => RoomCapacity);
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
                NotifyOfPropertyChange(() => DateOut);
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
            }
        }


        public decimal CalculateSubTotal()
        {
           return RoomPrice * StayDays;
        }


        public decimal CalculateTax()
        {
            string taxValue = ConfigurationManager.AppSettings["taxRate"];

            decimal taxRate = Decimal.Parse(taxValue);

            decimal subTotal = CalculateSubTotal();

            return subTotal * taxRate;
        }


        private string _subTotal;
       
        public string SubTotal
        {
            get { return CalculateSubTotal().ToString("C"); }
            set
            {
                _subTotal = value;
                NotifyOfPropertyChange(() => SubTotal);               
            }
        }


        private string _tax;

        public string Tax
        {
            get { return CalculateTax().ToString("C"); }
            set
            {
                _tax = value;
                NotifyOfPropertyChange(() => Tax);
            }
        }



        private string _total;
        public string Total
        {
            get
            {
                decimal total =  CalculateSubTotal() + CalculateTax();
                return total.ToString("C");
            }
            set
            {
                _total = value;
                NotifyOfPropertyChange(() => Total);
            }
        }



        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                if (CheckedIn != null && CheckedIn.IsCheckedOut == false)
                {
                    output = true;
                }

                return output;
            }
        }


        public async Task CheckOut()
        {
            CheckOutModel checkOut = new CheckOutModel
            {
                CheckInID = CheckedIn.Id,
                SubTotal = CalculateSubTotal(),
                Tax = CalculateTax(),
                Total = CalculateSubTotal() + CalculateTax()
            };

            await _checkOutEndpoint.PostCheckOut(checkOut);

            RoomModel room = new RoomModel
            {
                RoomNumber = CheckedIn.RoomNumber
            };

            RoomModel checkedInRoom = await _roomEndpoint.GetRoom(room);
            checkedInRoom.IsAvailable = true;
            await _roomEndpoint.UpdateRoom(checkedInRoom);

            CheckedIn.IsCheckedOut = true;
            await _checkInEndpoint.UpdateCheckIn(CheckedIn);

            ClearCheckOut();
        }



        public void ClearCheckOut()
        {
            CheckedIn = null;
            ErrorMessage = "";
            Client = "";
            Phone = "";
            RoomType = "";
            RoomNumber = 0;
            RoomCapacity = 0;
            RoomPrice = 0;
            DateIn = DateTime.Now;
            DateOut = DateTime.Now;
            StayDays = 0;
            GuestNumber = 0;
            SubTotal = "";
            Tax = "";
            Total = "";
        }
    }
}
