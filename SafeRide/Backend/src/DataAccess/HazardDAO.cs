﻿using CoordinateSharp;
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
        private string _cs = "Server=tcp:updatedbackend.database.windows.net,1433;Initial Catalog=UpdatedDatabase;Persist Security Info=False;User ID=colincreasman;Password=saferide.714;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=100;";
        private ApplicationUser _user;

        public HazardDAO()
        {
            this._user = new ApplicationUser();
        }

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

                    string queryString = $"SELECT longitude, latitude FROM Hazards WHERE hazardType = {hazardType} AND (acos(sin(latitude * 0.0175) * sin({targetX} * 0.0175) + cos(latitude * 0.0175) * cos({targetX} * 0.0175) * cos(({targetY} * 0.0175) - (longitude * 0.0175)) - 0.0000000000000000000000001) * 3959) <= {RADIUS_MILES}";
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
                   // conn.Close();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return results;
            }
            return results;
        }


        public int Report(Hazard hazard)
        {
            int numRowsAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_cs))
                {
                    conn.Open();
                    string queryString = "INSERT INTO Hazards(userHash, dateReported, hazardType, latitude, longitude, state, city, zip) VALUES (@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8)";
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


        // *** FOR DEBUGGING ONLY ***
        // queries database for all hazard types and their coordinates within a given search radius
        public Dictionary<int, double> GetAllHazardsInRadius(double targetY, double targetX, double radius)
        {
            // initialize empty dictionary of doubles to store the set of queried coordinates
            Dictionary<int, double> results = new Dictionary<int, double>();
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

                    //string queryString = $"SELECT hazardType, longitude, latitude FROM Hazards WHERE (acos(sin(latitude * 0.0175) * sin({targetX} * 0.0175) + cos(latitude * 0.0175) * cos({targetX} * 0.0175) * cos(({targetY} * 0.0175) - (longitude * 0.0175)) - 0.0000000000000000000000001) * 3959) <= {RADIUS_MILES}";
                    //Console.WriteLine(queryString);
                    string queryString = "SELECT hazardType, longitude FROM Hazards";

                    using (SqlCommand cmd = new SqlCommand(queryString, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int hazardType = (int)(reader["hazardType"] ?? 0);
                                //double[] location = new double[2];
                               double location = (double)(reader["longitude"] ?? 0);
                               // location[1] = (double)(reader["latitude"] ?? 0);
                                results.Add(hazardType, location);
                            }
                          //  reader.NextResult();
                        }
                    }
                    //conn.Close();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return results;
            }
            return results;
        }
    }
}

