
namespace SafeRide.src.Services
{
    public interface ISavedRouteService
    {
        public int AddSavedRoute(string UserID, int routeId, string route);
        public List<string> GetRecentRoutes(string userId);
        public List<string> GetSavedRoutes(string userId);    
        public Task<string> GetRouteData(string startLocation, string endLocation);
    }
}