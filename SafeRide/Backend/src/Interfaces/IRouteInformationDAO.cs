using SafeRide.src.Models;

namespace SafeRide.src.Interfaces
{
    public interface IRouteInformationDAO
    {
        // public bool searchRoute(string startpoint, string endpoint, string instructions, string username);
        public List<string> getRouteHistory(string UserName);

    }
}
