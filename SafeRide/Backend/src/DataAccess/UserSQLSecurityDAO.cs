using System.Data;
using System.Data.SqlClient;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SafeRide.src.DataAccess;

public class UserSQLSecurityDAO : IUserSecurityDAO
{
    private SqlConnectionStringBuilder builder;
    private const string TABLE_NAME = "UserSecurity";
    //private string _cs = "Server=tcp:updatedbackend.database.windows.net,1433;Initial Catalog=UpdatedDatabase;Persist Security Info=False;User ID=colincreasman;Password=saferide.714;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    public UserSQLSecurityDAO(IConfiguration config)
    {
        builder = new SqlConnectionStringBuilder();
        builder.DataSource = "updatedbackend.database.windows.net";
        builder.UserID = "colincreasman";
        builder.Password = "saferide.714";
        builder.InitialCatalog = "UpdatedDatabase";
    }
    
    private bool ExecuteCommand(SqlCommand cmd)

    {
        try
        {
            using (var sqlConn = new SqlConnection(builder.ConnectionString))
            {
                cmd.Connection = sqlConn;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message); // logger here
            return false;
        }
    }
    
    public bool Create(UserSecurityModel user)
    {
        string query = $"INSERT INTO {TABLE_NAME} (username, email, role, valid) VALUES" +
                       $" ('{user.UserName}', '{user.Email}', '{user.Role}', '1');";

        SqlCommand command = new SqlCommand();
        command.CommandText = query;
        return ExecuteCommand(command);
    }

    public UserSecurityModel Read(string username)
    {
        string query = $"SELECT * FROM {TABLE_NAME} WHERE username='{username}'";

        UserSecurityModel readModel = new UserSecurityModel();

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
                            readModel.UserName = (reader["username"].ToString() ?? "").Trim();
                            readModel.Email = (reader["email"].ToString() ?? "").Trim();
                            readModel.Role = (reader["role"].ToString() ?? "").Trim();
                            readModel.Valid = reader["valid"].Equals("1"); // defaults to false
                        }
                    }
                }
            }
        }
           
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return readModel;
        }

        return readModel;
    }

    public bool Update(string username, UserSecurityModel user)
    {
        string query = $"update {TABLE_NAME} set " +
                       $"username='{user.UserName}', email='{user.Email}',role='{user.Role}', valid='{user.Valid}' " +
                       $"where username='{username}';";

        SqlCommand command = new SqlCommand();
        command.CommandText = query;
        return ExecuteCommand(command);
    }

    public bool Delete(string username)
    {
        string query = $"DELETE FROM {TABLE_NAME} WHERE username = '{username}'";
        SqlCommand command = new SqlCommand();
        command.CommandText = query;
        return ExecuteCommand(command);
    }

    public bool Disable(string username)
    {
        SqlCommand command = new SqlCommand();
        command.CommandText = $"UPDATE {TABLE_NAME} SET valid='False' where username='{username}'";
        return ExecuteCommand(command);
    }

    public bool Enable(string username)
    {
        SqlCommand command = new SqlCommand();
        command.CommandText = $"UPDATE {TABLE_NAME} SET valid='True' where username='{username}'";
        return ExecuteCommand(command);
    }
}