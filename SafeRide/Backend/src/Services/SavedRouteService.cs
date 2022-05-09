using Backend.src.DataAccess;
using Backend.src.Models;

namespace Backend.src.Services
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
