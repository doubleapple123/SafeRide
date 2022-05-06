using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.DataAccess;

namespace Backend.src.Services
{
    public class SavedRouteService
    {
        private ISavedRouteDAO _IsavedRouteDAO;
        HttpClient client = new HttpClient();
        private string api_key = "";
        private string baseUrl = "";
        public SavedRouteService(ISavedRouteDAO savedRouteDAO)
        {
            _IsavedRouteDAO = savedRouteDAO;
        }
        public bool AddSavedRoute(string UserID, int routeId, string route)
        {
            return false;
        }

        public async Task GetRouteData()
        {
            string response = await client.GetStringAsync(
                ""
                );
        }

    }
}
