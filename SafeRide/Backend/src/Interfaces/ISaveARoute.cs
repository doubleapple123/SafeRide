using SafeRide.src.Models;

namespace SafeRide.src.Interfaces
{
    public interface ISaveARoute
    {

        public int AddSavedRoute(string userEmail, int routeId, string route, string tableName);
        public int DeleteSavedRoute(string userEmail, int routeId, string tableName);
        public List<string> GetSavedRoutes(string userEmail, string tablename);
        
       
    }
}
