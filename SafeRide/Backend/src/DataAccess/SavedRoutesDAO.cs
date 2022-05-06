using System.Data.SqlClient;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SafeRide.src.DataAccess
{
    public class SavedRoutesDAO : ISavedRouteDAO
    {
        private string _cs = "Server=tcp:saferidewithus.database.windows.net,1433;Initial Catalog=SafeRideDB;User ID=andyadmin;Password=Whoaman!123";
        private const string TABLE_NAME = "UserRoutes";

        public bool AddSavedRoute(string UserId, int routeId, string route)
        {
            string query = $"INSERT INTO {TABLE_NAME} values ( {UserId}, {routeId}, {route})";
            return ExecuteQuery.ExecuteCommand(_cs, query);
        }

       
    }

   

}
