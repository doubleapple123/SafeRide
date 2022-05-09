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
        private const string TABLE_NAME = "startpoint";

        public RouteHistoryDAO(IConfiguration config)
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "saferidedb.database.windows.net";
            builder.UserID = "azureuser";
            builder.Password = config["passkey123'"];
            builder.InitialCatalog = "SafeRide_DB";
        }

        public bool searchRoute(string UserName, string startpoint, string endpoint, string instructions)
        {
            string query = $"INSERT INTO {TABLE_NAME} values ('{startpoint}','{endpoint}','{instructions}'";
            return ExecuteQuery.ExecuteCommand(builder.ConnectionString, query);
        }
        public List<RouteInformation> getRouteHistory(string UserName)
        {
            var routeInfo = new List<RouteInformation>();
            string query = $"SELECT startpoint FROM {TABLE_NAME} WHERE UserName='{UserName}'";
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
                                var route = new RouteInformation(start, end, instructions);
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
