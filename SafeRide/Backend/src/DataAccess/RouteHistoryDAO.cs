using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.DataAccess;

namespace SafeRide.src.DataAccess
{
    public class RouteHistoryDAO
    {
        private string _cs = "Server=tcp:saferidedb.database.windows.net,1433;Initial Catalog=SafeRide_DB;Persist Security Info=False;User ID=azureuser;Password={;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private string TABLE_NAME = "RoutesHistory";
/*
        public bool searchRoute(string startpoint, string endpoint, string instructions, string username)
        {
            string query = $"INSERT INTO {TABLE_NAME} values ('{startpoint}','{endpoint}','{instructions}', '{username}'";
            return ExecuteQuery.ExecuteCommand(_cs, query);
        }*/
        public List<string> getRouteHistory(string UserName)
        {
            var recentRoutes = new List<string>();
            string query = $"SELECT routes FROM {TABLE_NAME} WHERE username='{UserName}'";
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_cs))
                {
                    sqlConn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                    {
                        // cmd.Parameters.Add(UserName, System.Data.SqlDbType.VarChar, 50).Value = UserName;
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var route = reader["route"].ToString();
                                recentRoutes.Add(route);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log error
            }

            return recentRoutes;
        }
    }

}
