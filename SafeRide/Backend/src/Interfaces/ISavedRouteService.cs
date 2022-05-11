 
namespace SafeRide.src.Services
{
    public interface ISavedRouteService
    {
        public int AddSavedRoute(string userEmail, int routeId, string route, string tableName);
        public int DeleteSavedRoute(string userEmail, int routeId, string tableName);
        public List<string> GetSavedRoutes(string userEmail, string tableName);    
        
    }
}