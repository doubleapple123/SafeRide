using Backend.src.Models;

namespace Backend.src.Interfaces
{
    public interface IRouteInformationDAO
    {
        public bool searchRoute(string startpoint, string endpoint, string instructions, string username);
        public List<RouteInformation> getRouteHistory(string UserName);

    }
}
