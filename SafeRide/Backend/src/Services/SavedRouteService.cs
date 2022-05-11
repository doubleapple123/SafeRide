using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.DataAccess;

namespace SafeRide.src.Services
{
    public class SavedRouteService : ISavedRouteService
    {
        private ISavedRouteDAO _isavedRouteDAO;
      
        private static HttpClient _httpClient;
        private string api_key = "";
        private string baseUrl = "";


        public SavedRouteService(ISavedRouteDAO isavedRouteDAO)
        {
            _isavedRouteDAO = isavedRouteDAO;
            _httpClient = new HttpClient();
        }




        //<Summary>
        //@param string UserID
        //@param int routeID
        //@param string route
        //
        //Method to add saved route to Database.
        //<Summary>
        public int AddSavedRoute(string userEmail, int routeId, string route, string tableName)
        {
            return _isavedRouteDAO.AddSavedRoute(userEmail, routeId, route, tableName);
        }

        public List<string> GetSavedRoutes(string userEmail, string tableName)
        {
            return _isavedRouteDAO.GetSavedRoutes(userEmail, tableName);
        }

        public int DeleteSavedRoute(string userEmail, int routeId, string tableName)
        { 
            return _isavedRouteDAO.DeleteSavedRoute(userEmail, routeId, tableName); 
        }

        // <Summary>
        // @param string Start Location as coordinates
        // @Param string End Location as coordinates
        // Method makes call to mapBox directions API to retrieve json response
        // </summary>
        public async Task<string> GetRouteData(string routeUrl)
        {
          
            var request = new HttpRequestMessage(HttpMethod.Get,$"{routeUrl}");
            var response = await _httpClient.SendAsync(request);

            return await response.Content.ReadAsStringAsync();


        }



    }
}
