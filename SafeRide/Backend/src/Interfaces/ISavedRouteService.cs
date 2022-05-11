
namespace SafeRide.src.Services
{
    public interface ISavedRouteService
    {
        public int AddSavedRoute(string UserID, int routeId, string route, string tableName);
        public List<string> GetRecentRoutes(string userId, string tableName);
        public List<string> GetSavedRoutes(string userId, string tableName);    
        public Task<string> GetRouteData(string routeUrl, string tableName);
    }
}