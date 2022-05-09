using Backend.src.DataAccess;

namespace Backend.src.Services
{
    public class SavedRouteService
    {
        private RouteHistoryDAO _ISavedRouteDAO;

        public SavedRouteService(RouteHistoryDAO iSavedRouteDAO)
        {
            _ISavedRouteDAO = iSavedRouteDAO;
        }

        public List<string> GetAllRoutes(string UserName)
        {
            var routeList = _ISavedRouteDAO.getRouteHistory(UserName);
            return routeList;
        }

     }
}
