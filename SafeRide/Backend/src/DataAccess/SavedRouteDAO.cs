using System.Data.SqlClient;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SafeRide.src.DataAccess;

public class SavedRouteDAO : ISavedRouteDAO
{
    private SqlConnectionStringBuilder builder;
    private const string TABLE_NAME = "SavedRoutes";
        
    public SavedRouteDAO(IConfiguration config)
    {
        builder = new SqlConnectionStringBuilder();
        builder.DataSource = "saferidesql.database.windows.net";
        builder.UserID = "saferideapple";
        builder.Password = "t^E~eT1+$~O5qjY6mS`PTVY=N$pOiNNR";
        builder.InitialCatalog = "SafeRide_DB";
    }
    public string GetSavedRoute(string UserName, string RouteName)
    {
        string query = $"SELECT RouteEncoded FROM {TABLE_NAME} where UserName='{UserName}' and RouteName='{RouteName}'";
        var encodedRoute = "";
        try
        {
            using (var sqlConn = new SqlConnection(builder.ConnectionString))
            {
                using (var cmd = new SqlCommand(query, sqlConn))
                {
                    cmd.Connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            encodedRoute = reader["RouteEncoded"].ToString();
                        }
                    }
                }
            }
        }
        catch
        {
            
        }

        return encodedRoute;
    }

    public bool AddSavedRoute(string UserName, string RouteName, string EncodedRoute)
    {
        string query = $"INSERT INTO {TABLE_NAME} values ('{UserName}', '{RouteName}', '{EncodedRoute}')";
        return ExecuteQuery.ExecuteCommand(builder.ConnectionString, query);
    }

    public List<string> GetAllSavedRoutes(string UserName)
    {
        string query = $"SELECT RouteName, RouteEncoded from {TABLE_NAME} where UserName='{UserName}'";
        var returnList = new List<string>();

        try
        {
            using (var sqlConn = new SqlConnection(builder.ConnectionString))
            {
                using (var cmd = new SqlCommand(query, sqlConn))
                {
                    cmd.Connection.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var routeName = reader["RouteName"].ToString();
                            /*
                            var encodedRoute = reader["RouteEncoded"].ToString();
                            */

                            /*
                            var routeObj = new SavedRoute(routeName, encodedRoute);
                            */
                            returnList.Add(routeName);
                        }
                    }
                }
            }
        }
        catch
        {
            
        }

        return returnList;
    }
}