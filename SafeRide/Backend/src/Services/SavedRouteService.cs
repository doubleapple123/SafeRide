using SafeRide.src.DataAccess;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SafeRide.src.Services
{
    public class SavedRouteService : ISaveRouteService
    {
        private RouteHistoryDAO _ISavedRouteDAO;
        public SavedRouteService(RouteHistoryDAO iSavedRouteDAO)
        {
            _ISavedRouteDAO = iSavedRouteDAO;
        }

        public List<string> GetAllRoutes(string UserName, string tableName)
        {
            return _ISavedRouteDAO.getRouteHistory(UserName, tableName);
        }

     }
}
