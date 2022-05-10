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
        private SqlConnectionStringBuilder builder;
        private const string TABLE_NAME = "RouteInfo";

        public RouteHistoryDAO(IConfiguration config)
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "saferidedb.database.windows.net";
            builder.UserID = "azureuser";
            builder.Password = config["passkey123'"];
            builder.InitialCatalog = "SafeRide_DB";
        }

        public bool searchRoute(string startpoint, string endpoint, string instructions, string username)
        {
            string query = $"INSERT INTO {TABLE_NAME} values ('{startpoint}','{endpoint}','{instructions}', '{username}'";
            return ExecuteQuery.ExecuteCommand(builder.ConnectionString, query);
        }
        public List<RouteInformation> getRouteHistory(string UserName)
        {
            var routeInfo = new List<RouteInformation>();
            string query = $"SELECT startpoint, endpoint, instructions, username FROM {TABLE_NAME} WHERE username='{UserName}'";
            try
            {
                using (var sqlConn = new SqlConnection(builder.ConnectionString))
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
