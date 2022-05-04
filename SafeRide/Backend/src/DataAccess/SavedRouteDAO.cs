using System.Data.SqlClient;
using SafeRide.src.Interfaces;

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
        builder.Password = config["AppKey:DBKey"];
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
                    cmd.Connection.OpenAsync();

                    using (var reader = cmd.ExecuteReader())
                    {
                        encodedRoute = reader["RouteEncoded"].ToString();
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
        throw new NotImplementedException();
    }
}