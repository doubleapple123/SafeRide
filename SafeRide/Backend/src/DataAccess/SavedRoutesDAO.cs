using System.Data.SqlClient;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SafeRide.src.DataAccess
{
    public class SavedRoutesDAO : ISavedRouteDAO
    {
        private string _cs = "Server=tcp:saferidewithus.database.windows.net,1433;Initial Catalog=SafeRideDB;User ID=andyadmin;Password=Whoaman!123";
        private const string TABLE_NAME = "UserRoutes";


        public string GetRecentRoutes(string userId)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(_cs))
                {
                    conn.Open();
                    string query = $"SELECT route WHERE userID= {userId}";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               var route = reader[""];
                    }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                return "error fetching file";
            }
        }

        public string GetSavedRoutes(string userId, int routeId, string route)
        {
         }
        public int AddSavedRoute(string UserId, int routeId, Route route)
        {
            int numRowsAffected = -1;
            using (SqlConnection conn = new SqlConnection(_cs)) { 
                conn.Open();
                string query = "INSERT INTO UserRoute (userID, routeID, route) VALUES (@param1, @param2, @param3)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
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
