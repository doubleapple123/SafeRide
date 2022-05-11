using SafeRide.src.DataAccess;
using SafeRide.src.Models;

namespace SafeRide.src.Services
{
    public class SavedRouteService
    {
        private RouteHistoryDAO _ISavedRouteDAO;
        public SavedRouteService(RouteHistoryDAO iSavedRouteDAO)
        {
            _ISavedRouteDAO = iSavedRouteDAO;
        }

        public List<RouteInformation> GetAllRoutes(string UserName)
        {
            var routeList = _ISavedRouteDAO.getRouteHistory(UserName);
            return routeList;
        }

     }
}
