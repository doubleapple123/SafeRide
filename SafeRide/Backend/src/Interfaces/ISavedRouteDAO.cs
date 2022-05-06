using SafeRide.src.Models;

namespace SafeRide.src.Interfaces
{
    public interface ISavedRouteDAO
    {

        public bool AddSavedRoute(string userId, int routeId, string route);
        

    }
}
