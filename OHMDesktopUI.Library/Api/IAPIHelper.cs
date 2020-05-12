using System.Net.Http;
using System.Threading.Tasks;
using OHMDesktopUI.Library.Models;

namespace OHMDesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }
        Task<AuthenticatedUser> Authenticate(string userName, string password);
        Task GetLoggedInUserInfo(string token);
        void LogOffUser();
    }
}