using System.Data.SqlClient;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SafeRide.src.DataAccess
{
    public class SavedRoutesDAO : ISavedRouteDAO
    {
        private string _cs = "Server=tcp:saferidewithus.database.windows.net,1433;Initial Catalog=SafeRideDB;User ID=andyadmin;Password=Whoaman!123";
        private const string TABLE_NAME = "UserRoutes";


        public List<string> GetRecentRoutes(string userId, string tableName)
        {
            var listOfRoute = new List<string>();
            try
            {

                using (SqlConnection conn = new SqlConnection(_cs))
                {
                    conn.Open();
                    string query = $"SELECT { tableName } FROM userTempRoute WHERE userID= @param1";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@param1", System.Data.SqlDbType.Int).Value = userId;
                        
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

        public List<string> GetSavedRoutes(string userId, string tableName)
        {
            var listOfRoute = new List<string>();
            try
            {

                using (SqlConnection conn = new SqlConnection(_cs))
                {
                    conn.Open();
                    string query = $"SELECT {tableName} FROM userRoutes WHERE userID= @param1";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@param1", System.Data.SqlDbType.Int).Value = userId;
                       

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
        public int AddSavedRoute(string userId, int routeId, string route, string tableName)
        {
            int numRowsAffected = -1;
            using (SqlConnection conn = new SqlConnection(_cs)) { 
                conn.Open();
                string query = $"INSERT INTO {tableName} VALUES @param1 @param2 @param3)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@param1", System.Data.SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@param2", System.Data.SqlDbType.Int).Value = routeId;
                    cmd.Parameters.Add("@Param3", System.Data.SqlDbType.VarChar, 200).Value = route;
                    numRowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return numRowsAffected;
        }

       
    }

   

}
