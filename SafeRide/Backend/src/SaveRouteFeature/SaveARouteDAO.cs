using System.Data.SqlClient;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SafeRide.src.DataAccess
{
    public class SaveARouteDAO :  ISaveARoute
    {
        private string _cs = "Server=tcp:saferidewithus.database.windows.net,1433;Initial Catalog=SafeRideDB;Persist Security Info=False;User ID=andyadmin;Password=Whoaman!123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";



        /*
         <summary>
        GetSavedRoutes executes SQL command and populates array grabbing @Param routes
        returns List of routes from database
         
         */
        public List<string> GetSavedRoutes(string userEmail, string tableName)
        {
            var listOfRoute = new List<string>();
            try
            {

                using (SqlConnection conn = new SqlConnection(_cs))
                {
                    conn.Open();
                    string query = $"SELECT routeName FROM { tableName } WHERE userEmail= @param1";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@param1", System.Data.SqlDbType.VarChar, 50).Value = userEmail;
                        

                        using (var reader = cmd.ExecuteReader())
                        { 
                            while (reader.Read())
                            {
                                var route = reader["routeName"].ToString();

                                listOfRoute.Add(route);
                            }
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {

            }

            return listOfRoute;
        }


        /*'
         Summary
         @param userEmail
         @param routeID
         @param routeName
         @param tableName
            Method adds selected route in the format of the maprequest API call.
          
         returns returns number of rows affected, which should reflect one row changed.
         */
        public int AddSavedRoute(string userEmail, int routeId, string routeName, string tableName)
        {
            int numRowsAffected = -1;
            using (SqlConnection conn = new SqlConnection(_cs)) { 
                conn.Open();
                string query = $"INSERT INTO {tableName} VALUES (@param1, @param2, @param3)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@param1", System.Data.SqlDbType.VarChar, 50).Value = userEmail;
                    cmd.Parameters.Add("@param2", System.Data.SqlDbType.Int).Value = routeId;
                    cmd.Parameters.Add("@Param3", System.Data.SqlDbType.VarChar, 500).Value = routeName;
                    numRowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return numRowsAffected;
        }



        /*'
         Summary
         @param userEmail
         @param routeID
         @param routeName
         @param tableName
            Method to detele selected route from user database.
          
         returns returns number of rows affected, which should reflect one row changed.
         */
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
