using CoordinateSharp;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using System.Data;
using System.Data.SqlClient;

namespace SafeRide.src.DataAccess
{
    public class HazardDAO : IHazardDAO
    {
        // TODO Uncomment when done unit testing
        //private string _cs = System.Configuration.ConfigurationManager.ConnectionStrings["SafeRideDB"].ConnectionString;
        private string _cs = "Server=tcp:updatedbackend.database.windows.net,1433;Initial Catalog=UpdatedDatabase;Persist Security Info=False;User ID=colincreasman;Password=saferide.714;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private ApplicationUser _user;

        public HazardDAO()
        {
            this._user = new ApplicationUser();
        }


        // make open async method
        public Dictionary<double, double> GetByTypeInRadius(int hazardType, double targetY, double targetX, double radius)
        {
            // initialize empty dictionary of doubles to store the set of queried coordinates
            Dictionary<double, double> results = new Dictionary<double, double>();
            // convert meters from meters to miles
            double RADIUS_MILES = radius * 0.000621371;

            // attempt connecting to the database to query for mathing hazards
            try
            {
                using (SqlConnection conn = new SqlConnection(_cs))
                {
                    conn.Open();
                    // build query using trigonometry function to search for the coordinates of all hazards of the provided type within the set radius around a coordinate defined by the provided targetX and targetY values
                    Console.WriteLine("successfully connected");

                    string queryString = $"SELECT longitude, latitude FROM Hazards WHERE hazardType = {hazardType} AND (acos(sin(latitude * 0.0175) * sin({targetX} * 0.0175) + cos(latitude * 0.0175) * cos({targetX} * 0.0175) * cos(({targetY} * 0.0175) - (longitude * 0.0175)) - 0.0000000000001) * 3959) <= {RADIUS_MILES}";
                    //Console.WriteLine(queryString);

                    using (SqlCommand cmd = new SqlCommand(queryString, conn))
                    {
                        // make execute reader async
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // add each set of queried coordinates to the return dictionary
                                double hazardY = (double)(reader["longitude"] ?? 0);
                                double hazardX = (double)(reader["latitude"] ?? 0);

                                try
                                {
                                    results.Add(hazardY, hazardX); 
                                }

                                // Catch ArguementException that is thrown when adding an item with a key that is already in result 
                                catch (System.ArgumentException ex)
                                {
                                    // It's possible that multiple queried hazards might have the same longitude but still be in different locations
                                    // In this case the coordinates should still be added to the results set as they represent a distinct location from the other coordinates with the matching longitude
                                    // before ignoring the exception, first check if the current location also has a matching latitude
                                    if (results.TryGetValue(hazardY, out hazardX))
                                    {
                                        // if true, skip adding the current coordinates to the results set because it already has a matching latitiude for this longitude
                                        continue;
                                    }
                                    // otherwise, ignore the exception
                                }
                            }
                        }
                    }
                    conn.Close();
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return results;
            }

            return results;
        }


        public Dictionary<double, double> GetAllHazardCoordinates()
        {
            // initialize empty dictionary of doubles to store the set of queried coordinates
            Dictionary<double, double> results = new Dictionary<double, double>();
            
            // attempt connecting to the database to query for mathing hazards
            try
            {
                using (SqlConnection conn = new SqlConnection(_cs))
                {
                    conn.Open();
                    // build query using trigonometry function to search for the coordinates of all hazards of the provided type within the set radius around a coordinate defined by the provided targetX and targetY values
                    //Console.WriteLine("successfully connected");

                    string queryString = $"SELECT longitude, latitude FROM Hazards;";
                    //Console.WriteLine(queryString);

                    using (SqlCommand cmd = new SqlCommand(queryString, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // add each set of queried coordinates to the return dictionary
                                double hazardY = (double)(reader["longitude"] ?? 0);
                                double hazardX = (double)(reader["latitude"] ?? 0);
                                results.Add(hazardY, hazardX);
                            }
                        }
                    }
                    conn.Close();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return results;
            }

            return results;
        }




        /// <summary>
        /// Inserts given hazard into Hazards table
        /// </summary>
        /// <param name="hazard">Hazard to be inserted</param>
        /// <returns>Number of rows affected by insert</returns>
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

        /// <summary>
        /// Gets all hazards that are not expired
        /// </summary>
        /// <returns>List of non-expired hazards</returns>
        /// 
        //execute stored procedure rather than raw query string

        //reflection/code introspection
        public List<Hazard> GetAllValidHazards()
        {
            List<Hazard> hazardList = new List<Hazard>();
            using (SqlConnection conn = new SqlConnection(_cs))
            {
                conn.Open();
                string queryString = "SELECT * FROM Hazards WHERE  dateReported AT TIME ZONE 'PACIFIC STANDARD TIME' AT TIME ZONE 'UTC' between DATEADD(hh, -24, GETUTCDATE()) and GETUTCDATE()";
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


        // *** FOR DEBUGGING ONLY ***
        // queries database for all hazard types and their coordinates within a given search radius
        //public Dictionary<int, double> GetAllHazardsInRadius(double targetY, double targetX, double radius)
        //{
        //    // initialize empty dictionary of doubles to store the set of queried coordinates
        //    Dictionary<int, double> results = new Dictionary<int, double>();
        //    // convert meters from meters to miles
        //    double RADIUS_MILES = radius * 0.000621371;

        //    // attempt connecting to the database to query for mathing hazards
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(_cs))
        //        {
        //            conn.Open();
        //            // build query using trigonometry function to search for the coordinates of all hazards of the provided type within the set radius around a coordinate defined by the provided targetX and targetY values
        //            Console.WriteLine("successfully connected");

        //            //string queryString = $"SELECT hazardType, longitude, latitude FROM Hazards WHERE (acos(sin(latitude * 0.0175) * sin({targetX} * 0.0175) + cos(latitude * 0.0175) * cos({targetX} * 0.0175) * cos(({targetY} * 0.0175) - (longitude * 0.0175)) - 0.0000000000000000000000001) * 3959) <= {RADIUS_MILES}";
        //            //Console.WriteLine(queryString);
        //            string queryString = "SELECT longitude, hazardType FROM Hazards;";

        //            using (SqlCommand cmd = new SqlCommand(queryString, conn))
        //            {
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        //double[] location = new double[2];
        //                        double location = (double)(reader["longitude"] ?? 0);
        //                        int hazardType = (int)(reader["hazardType"] ?? 0);
        //                        // location[1] = (double)(reader["latitude"] ?? 0);
        //                        results.Add(hazardType, location);
        //                    }
        //                  //  reader.NextResult();
        //                }
        //            }
        //            //conn.Close();
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return results;
        //    }
        //    return results;
        //}
    }
}

