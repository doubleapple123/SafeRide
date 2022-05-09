using System.Data.SqlClient;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SafeRide.src.DataAccess
{
    public class SavedRoutesDAO : ISavedRouteDAO
    {
        private string _cs = "Server=tcp:saferidewithus.database.windows.net,1433;Initial Catalog=SafeRideDB;User ID=andyadmin;Password=Whoaman!123";
        private const string TABLE_NAME = "UserRoutes";

        public int AddSavedRoute(string UserId, int routeId, string route)
        {
            int numRowsAffected = -1;
            using (SqlConnection sqlConn = new SqlConnection(_cs)) { 
                sqlConn.Open();
                string query = "INSERT INTO UserRoute (userID, routeID, route) VALUES (@param1, @param2, @param3)";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                {
                    cmd.Parameters.Add("@param1", System.Data.SqlDbType.VarChar, 28).Value = UserId;
                    cmd.Parameters.Add("@param2", System.Data.SqlDbType.Int).Value = routeId;
                    cmd.Parameters.Add("@Param3", System.Data.SqlDbType.VarChar, 8000).Value = route;
                    numRowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return numRowsAffected;
        }

       
    }

   

}
