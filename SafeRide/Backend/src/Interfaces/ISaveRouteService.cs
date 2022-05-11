using SafeRide.src.Models;

namespace SafeRide.src.Interfaces
{
    public interface ISaveRouteService
    {
        public List<RouteInformation> GetAllRoutes(string UserName);
    }
}
