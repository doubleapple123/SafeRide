using System.Data.SqlClient;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SafeRide.src.DataAccess
{
    public class SavedRoutesDAO : ISavedRoutesDAO
    {
        private string _cs = "Server=tcp:saferidewithus.database.windows.net,1433;Initial Catalog=SafeRideDB;User ID=andyadmin;Password=Whoaman!123";

        public List<SavedRoutes> GetRouteHistory(string userName)
        {
            string query = "";
            var returnList = new List<SavedRoutes>();

            try
            {

            }
            catch 
            {
            
            }
            return returnList;
        }
    }

   

}
