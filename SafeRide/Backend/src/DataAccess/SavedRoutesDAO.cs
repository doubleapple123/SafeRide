using System.Data.SqlClient;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SafeRide.src.DataAccess
{
    public class SavedRoutesDAO :  ISavedRouteDAO
    {
        private string _cs = "Server=tcp:saferidewithus.database.windows.net,1433;Initial Catalog=SafeRideDB;User ID=andyadmin;Password=Whoaman!123";
        private const string TABLE_NAME = "UserRoutes";


        public List<string> GetSavedRoutes(string userEmail, string tableName)
        {
            var listOfRoute = new List<string>();
            try
            {

                using (SqlConnection conn = new SqlConnection(_cs))
                {
                    conn.Open();
                    string query = $"SELECT route FROM {tableName} WHERE userEmail= @param1";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@param1", System.Data.SqlDbType.VarChar, 50).Value = userEmail;
                       

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var route = reader["route"].ToString();

                                listOfRoute.Add(route);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return listOfRoute;
        }
        public int AddSavedRoute(string userEmail, int routeId, string route, string tableName)
        {
            int numRowsAffected = -1;
            using (SqlConnection conn = new SqlConnection(_cs)) { 
                conn.Open();
                string query = $"INSERT INTO {tableName} VALUES (@param1, @param2, @param3)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@param1", System.Data.SqlDbType.VarChar, 50).Value = userEmail;
                    cmd.Parameters.Add("@param2", System.Data.SqlDbType.Int).Value = routeId;
                    cmd.Parameters.Add("@Param3", System.Data.SqlDbType.VarChar, 200).Value = route;
                    numRowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return numRowsAffected;
        }
        public int DeleteSavedRoute(string userEmail, int routeId, string tableName) 
        {
            int numRowsAffected = -1;
            using (SqlConnection conn = new SqlConnection(_cs))
            {
                conn.Open();
                string query = 
                    $"DELETE FROM {tableName} " +
                    $"WHERE userEmail= @param1 " +
                    $"AND routeID= @param2";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@param1", System.Data.SqlDbType.VarChar, 50).Value = userEmail;
                    cmd.Parameters.Add("@param2", System.Data.SqlDbType.Int).Value = routeId;
                    
                    numRowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return numRowsAffected;
        }

       
    }

   

}
