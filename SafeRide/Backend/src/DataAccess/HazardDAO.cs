using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using System.Data;
using System.Data.SqlClient;
using SafeRide.src.Models;

namespace SafeRide.src.DataAccess
{
    public class HazardDAO : IHazardDAO
    {
        private string _cs = "Data Source=tcp:saferidedbserver.database.windows.net,1433;Initial Catalog=SafeRide_db;User Id=SafeRideAdmin@saferidedbserver;Password=applepw123!";

        //private ApplicationUser _user;
        //private Hazard _hazard;

        //public HazardDAO() {
        //    this._user = new ApplicationUser();
        //}

        //public HazardDAO(ApplicationUser user, Hazard hazard) {

        //    this._user = user;
        //    this._hazard = hazard;
        //}

        public Dictionary<double, double> FindHazardInRadius(HazardType hazardType, double searchX, double targetY, double radius)
        {
            // initialize empty dictionary of doubles to store the set of queried coordinates
            return new Dictionary<double, double>();
            // convert meters from meters to miles
            //const float RADIUS_MILES = radius * 0.000621371;

            //// attempt connecting to the database to query for mathing hazards
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(_cs))
            //    {
            //        conn.Open();

            //        // build query using trigonometry function to search for the coordinates of all hazards of the provided type within the set radius around a coordinate defined by the provided targetX and targetY values
            //        string queryString = $"SELECT latitude, longitude FROM Hazards WHERE hazardType = {hazardType} AND (acos(sin(latitude * 0.0175) * sin({targetX} * 0.0175) + cos(latitude * 0.0175) * cos({targetX} * 0.0175) * cos(({targetY} * 0.0175) - ({targetY} * 0.0175)) * 3959 <= {RADIUS_MILES})";

            //        using (SqlCommand cmd = new SqlCommand(queryString, conn)) 
            //        {
            //            using (SqlDataReader reader = cmd.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    // add each set of queried coordinates to the return dictionary
            //                    double hazardX = (double) (reader["latitude"] ?? 0);
            //                    double hazardY = (double) (reader["longitude"] ?? 0);
            //                    foundCoordinates.Add(hazardX, hazardY);
            //                }
            //            }
            //        }
            //    }
            //}

            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return foundCoordinates;
            //}
            //return foundCoordinates;
        }

        public int Report(Hazard hazard)
        {
            int numRowsAffected = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(_cs))
                {
                    conn.Open();
                    string queryString = "INSERT INTO Hazards(userHash, dateReported, hazardType, latitude, longitude, state, city, zip, expired) VALUES (@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, 0)";
                    using (SqlCommand cmd = new SqlCommand(queryString, conn))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.VarChar, 64).Value = hazard.ReportedBy;
                        cmd.Parameters.Add("@param2", SqlDbType.DateTime).Value = hazard.TimeReported;
                        cmd.Parameters.Add("@param3", SqlDbType.Int).Value = hazard.Type;
                        cmd.Parameters.Add("@param4", SqlDbType.Float).Value = hazard.Latitude;
                        cmd.Parameters.Add("@param5", SqlDbType.Float).Value = hazard.Longitude;
                        cmd.Parameters.Add("@param6", SqlDbType.VarChar, 20).Value = hazard.State;
                        cmd.Parameters.Add("@param7", SqlDbType.VarChar, 50).Value = hazard.City;
                        cmd.Parameters.Add("@param8", SqlDbType.Int).Value = hazard.Zip;

                        numRowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return numRowsAffected;
            }
            //ExecuteNonQuery() returns the number of rows affected, ideally for this function it should be 1
            //we use this to check if the message was succesfully inserted into database
            return numRowsAffected;
        }

        public List<Hazard> GetAllValidHazards()
        {
            List<Hazard> hazardList = new List<Hazard>();
            using (SqlConnection conn = new SqlConnection(_cs))
            {
                conn.Open();
                string queryString = "SELECT * FROM Hazards WHERE expired = 0";
                using (SqlCommand cmd = new SqlCommand(queryString, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HazardType type = (HazardType)reader.GetInt32(3);
                            string user = reader.GetString(1);
                            double lat = reader.GetDouble(4);
                            double lon = reader.GetDouble(5);
                            string state = reader.GetString(6);
                            string city = reader.GetString(7);
                            int zip = reader.GetInt32(8);
                            int expired = reader.GetInt32(9);
                            DateTime time = reader.GetDateTime(2);
                            Hazard hazard = new Hazard(type, lat, lon, user, state, zip, city, time, expired);
                            hazardList.Add(hazard);
                        }
                    }
                }
            }
            return hazardList;
        }
    }
}

