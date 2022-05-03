﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.WebUtilities;
using Backend.Services;
using Xunit;
using SafeRide.src.Models;
using SafeRide.src.Services;
using Newtonsoft.Json;
using System;
using SafeRide.src.DataAccess;
using Xunit.Abstractions;


namespace SRUnitTests
{



    public class ExcludeHazardUnitTests
    {

        private readonly ITestOutputHelper output;

        public ExcludeHazardUnitTests(ITestOutputHelper output)
        {
            this.output = output;
        }


        // two random step coordinates taken from the route response used in this UnitTest
        // the actual actual distance between them is 10.820 miles (17413.807 meters)
        // calculated using "https://www.cqsrg.org/tools/GCDistance/"
        [Theory]
        [InlineData(-121.5519422062582, 37.77904292167199, -121.39962701098186, 37.711694879433495, 8046.72, false)] // test if the target coord is within a 5 mile radius of the center 
        [InlineData(-121.5519422062582, 37.77904292167199, -121.39962701098186, 37.711694879433495, 24140.2, true)] // test if the target coord is within a 15 mile radius of the center 
        public void IsInsideRadius(double centerY, double centerX, double targetY, double targetX, double radius, bool expected)
        {
            /// coordinates from mapbox come in the form (<long> (y),<lat> (x)), so they must be inverted when calling the function
			ExcludeHazardService excludeService = new ExcludeHazardService();
            var actual = excludeService.IsInside(centerY, centerX, targetY, targetX, radius);

            Assert.Equal(actual, expected);
        }


        [Fact]
        public async Task FindSearchCoordinates()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.mapbox.com/directions/v5/mapbox/driving/-73.832902%2C40.736744%3B-73.834413%2C40.736805?alternatives=true&geometries=geojson&language=en&overview=simplified&steps=true&access_token=pk.eyJ1IjoiY29saW5jcmVhc21hbiIsImEiOiJjbDIxbGhnZ2QxMW1pM2Jwamp4YW42M25zIn0.WJD2zPxATbnf2utML0OOCQ");

            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            ParseResponseService? ParseResponseService = new ParseResponseService();

            ParseResponseService.ParseResponse(jsonResponse);


            var firstRoute = ParseResponseService.GetRoute(0);

            ExcludeHazardService? excludeHazardService = new ExcludeHazardService(firstRoute);

            // all points of the route generated by the hard coded request URL  are within a single search radius
            // in this case, FindSearchCoordinates should only return one search coordinate where the route starts
            var expected = new Dictionary<double, double>()
            {
                [-73.832902] = 40.736744
            };
            var actual = excludeHazardService.FindSearchCoordinates();

            Assert.Equal(expected, actual);
        }

   
        [Theory]
        [InlineData(0, 5)] // expecting 5 "Accident" hazards within radius
        [InlineData(1, 1)] // expecting 1 "Obstruction" hazard within radius
        [InlineData(2, 3)] // expecting 3 "BikeLane" hazards within radius
        [InlineData(3, 4)] // expecting 4 "Vehicle" hazards within radius
        [InlineData(4, 0)] // expecting 0 "Closure" hazards within radius
        public void GetByTypeInRadius(int hazardType, int expected)
        {

            double[] testCoordinate = { -74.002917, 40.73992 };
            double radiusInMeters = 8046.72;
            HazardDAO hazardDAO = new HazardDAO();

             // use to check coordinates manually
            //Dictionary<int, double> allHazards = hazardDAO.GetAllHazardsInRadius(testCoordinate[0], testCoordinate[1], radiusInMeters);
            //output.WriteLine("\nActual coordinates queried for all hazard types within radius");
            //  output.WriteLine(allHazards.Count.ToString());
            //foreach (KeyValuePair<int, double> kvp in allHazards)
            //{
            //    output.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            //}


            // use for troubleshooting incorrect coordinate queries
            Dictionary<double, double> actualHazards = hazardDAO.GetByTypeInRadius(hazardType, testCoordinate[0], testCoordinate[1], radiusInMeters);
            output.WriteLine("\nActual coordinates queried for hazards with type {0} in radius:", hazardType);
            foreach (KeyValuePair<double, double> kvp in actualHazards)
            {
                output.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            int actual = actualHazards.Count;
            Assert.Equal(expected, actual);


            // use to show exact query results when troubleshooting DAO method
            //  Dictionary<double, double> expectedAccidentHazards = new Dictionary<double, double>()
            //  {
            //      [-73.99015] = 40.732204,
            //      [-73.990042] = 40.733456,
            //      [-73.990014] = 40.732818,
            //      [-73.989869] = 40.732689,
            //      [-73.990042] = 40.732689,

            //  };
            ////  Assert.NotStrictEqual(expectedHazards, actual);
            //  Assert.Equal(expectedAccidentHazards, actualHazards);
        }
    }
}


//public Dictionary<double, double> GetByTypeInRadius(int hazardType, double targetY, double targetX, double radius)
//{
//    // initialize empty dictionary of doubles to store the set of queried coordinates
//    Dictionary<double, double> results = new Dictionary<double, double>();
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

//            string queryString = $"SELECT longitude, latitude FROM Hazards WHERE hazardType = {hazardType} AND (acos(sin(latitude * 0.0175) * sin({targetX} * 0.0175) + cos(latitude * 0.0175) * cos({targetX} * 0.0175) * cos(({targetY} * 0.0175) - (longitude * 0.0175)) - 0.0000000000000000000000001) * 3959) <= {RADIUS_MILES}";
//            //Console.WriteLine(queryString);

//            using (SqlCommand cmd = new SqlCommand(queryString, conn))
//            {
//                using (SqlDataReader reader = cmd.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        // add each set of queried coordinates to the return dictionary
//                        double hazardY = (double)(reader["longitude"] ?? 0);
//                        double hazardX = (double)(reader["latitude"] ?? 0);

//                        results.Add(hazardY, hazardX);
//                    }
//                }
//            }
//            conn.Close();
//        }
//    }

//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.Message);
//        return results;
//    }
//    return results;
//}


