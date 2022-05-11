using SafeRide.src.Models;

namespace SafeRide.src.Interfaces
{
    public interface ISavedRouteDAO
    {

        public int AddSavedRoute(string userId, int routeId, string route, string tableName);
        public List<string> GetRecentRoutes(string userId, string tableName);
        public List<string> GetSavedRoutes(string userId, string tablename);
        
       
    }
}
