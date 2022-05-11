using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using Backend.src.Interfaces;
using Backend.src.Models;
using SafeRide.src.DataAccess;

namespace Backend.src.DataAccess
{
    public class RouteHistoryDAO
    {
        private string _cs = "Server=tcp:saferidedb.database.windows.net,1433;Initial Catalog=SafeRide_DB;Persist Security Info=False;User ID=azureuser;Password=passkey123';MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private const string TABLE_NAME = "RouteInfo";


        public bool searchRoute(string startpoint, string endpoint, string instructions, string username)
        {
            string query = $"INSERT INTO {TABLE_NAME} values ('{startpoint}','{endpoint}','{instructions}', '{username}'";
            return ExecuteQuery.ExecuteCommand(_cs, query);
        }
        public List<RouteInformation> getRouteHistory(string UserName)
        {
            var routeInfo = new List<RouteInformation>();
            string query = $"SELECT startpoint, endpoint, instructions, username FROM {TABLE_NAME} WHERE username='{UserName}'";
            try
            {
                using (var sqlConn = new SqlConnection(_cs))
                {
                    using (SqlCommand cmd = new SqlCommand(query, sqlConn))
                    {
                        cmd.Connection.Open();
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var start = reader["startpoint"].ToString();
                                var end = reader["endpoint"].ToString();
                                var instructions = reader["instructions"].ToString();
                                var username = reader["username"].ToString();
                                var route = new RouteInformation(start, end, instructions, username);
                                routeInfo.Add(route);
                            }
                        }
                    }
                }
            }
            catch
            {
                // log error
            }

            return routeInfo;
        }
    }

}
