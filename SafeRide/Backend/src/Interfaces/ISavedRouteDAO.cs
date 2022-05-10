using SafeRide.src.Models;

namespace SafeRide.src.Interfaces
{
    public interface ISavedRouteDAO
    {

        public int AddSavedRoute(string userId, int routeId, string route);
        public List<string> GetRecentRoutes(string userId);
        public List<string> GetSavedRoutes(string userId);
        
       
    }
}
