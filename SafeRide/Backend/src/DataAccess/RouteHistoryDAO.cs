using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace Backend.src.DataAccess
{
    public class RouteHistoryDAO
    {
        private SqlConnectionStringBuilder builder;
        private const string TABLE_NAME = "id";
        private const string SECOND_TABLE_NAME = "startpoint";

        public RouteHistoryDAO(IConfiguration config)
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "saferidedb.database.windows.net";
            builder.UserID = "azureuser";
            builder.Password = config["passkey123'"];
            builder.InitialCatalog = "SafeRide_DB";
        }

        public List<string> getRouteInfo(string startPoint)
        {
            var routeInfo = new List<string>();
            string query = $"SELECT startpoint FROM {SECOND_TABLE_NAME} WHERE startpoint='{startPoint}'";
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
                                var route = reader["startpoint"].ToString();
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

            return routeInfo.ToList();
        }
    }

}
