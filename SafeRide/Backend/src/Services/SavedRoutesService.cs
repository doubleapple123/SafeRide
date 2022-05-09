using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.DataAccess;

namespace Backend.src.Services
{
    public class SavedRouteService
    {
        private ISavedRouteDAO _isavedRouteDAO;
        HttpClient client = new HttpClient();
        private string api_key = "";
        private string baseUrl = "";
        public SavedRouteService(ISavedRouteDAO savedRouteDAO)
        {
            _isavedRouteDAO = savedRouteDAO;
        }
        public bool AddSavedRoute(string UserID, int routeId, string route)
        {
            return _isavedRouteDAO.AddSavedRoute(UserID,routeId,route);
        }

        public async Task GetRouteData(string startLocation, string endLocation)
        {
            var startCoordinate = startLocation.Split(", ");
            var endCoordinate = endLocation.Split(", ");

            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.mapbox.com/directions/v5/mapbox/cycling/${startCoordinate[0]},${startCoordinate[1]};${endCoordinate[0]},${endCoordinate[1]}?steps=true&geometries=geojson&access_token=${api_key}");
            HttpResponseMessage response = await client.SendAsync(request);
                
                );
            if (response.IsSuccessStatusCode)
                
            
        }

    }
}
