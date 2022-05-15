using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.DataAccess;

namespace SafeRide.src.Services
{
    public class SaveARouteService : ISaveARouteService
    {
        private ISaveARoute _iSaveARouteDAO;
        private ISaveARouteService _isaveARouteServie;
      
        private static HttpClient _httpClient;
        private string api_key = "pk.eyJ1IjoiY2FudGRyaW5rbWlsayIsImEiOiJjbDAwZnFiOHkwM3kyM3FwaG1qcmFhazh6In0.ytVFjAsRLDJra61yH0ZT-w"; 
        


        public SaveARouteService(ISaveARoute iSavedARouteDAO)
        {
            _iSaveARouteDAO = iSavedARouteDAO;
          
            
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
            return _iSaveARouteDAO.AddSavedRoute(userEmail, routeId, route, tableName);
        }

        public List<string> GetSavedRoutes(string userEmail, string tableName)
        {
            return _iSaveARouteDAO.GetSavedRoutes(userEmail, tableName);
        }

        public int DeleteSavedRoute(string userEmail, int routeId, string tableName)
        { 
            return _iSaveARouteDAO.DeleteSavedRoute(userEmail, routeId, tableName); 
        }

        // <Summary>
        // @param string Start Location as coordinates
        // @Param string End Location as coordinates
        // Method makes call to mapBox directions API to retrieve json response
        // </summary>
        //public async Task<string> GetRouteData(List<string> routeCoords)
        //{
        //    var requestURL =
        //        "https://api.mapbox.com/directions/v5/mapbox/cycling/${routeCoords[0]},${routeCoords[1]};${routeCoords[2]},${routeCoords[3]}?steps=true&geometries=geojson&access_token=${api_key}";


        //    var request = new HttpRequestMessage(HttpMethod.Get,$"{ requestURL}");
        //    var response = await _httpClient.SendAsync(request);

        //    return await response.Content.ReadAsStringAsync();


        //}
        


    }
}
