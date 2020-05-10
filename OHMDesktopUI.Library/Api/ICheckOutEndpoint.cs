using System.Threading.Tasks;
using OHMDesktopUI.Library.Models;

namespace OHMDesktopUI.Library.Api
{
    public interface ICheckOutEndpoint
    {
        Task PostCheckOut(CheckOutModel checkOut);
    }
}