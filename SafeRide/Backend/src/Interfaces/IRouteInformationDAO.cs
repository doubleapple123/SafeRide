using Backend.src.Models;

namespace Backend.src.Interfaces
{
    public interface IRouteInformationDAO
    {
        public RouteInformation GetRouteInformation(string userName);

    }
}
