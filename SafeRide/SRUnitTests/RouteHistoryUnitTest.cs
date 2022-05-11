using Xunit;
using Xunit.Abstractions;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.DataAccess;
using SafeRide.src.Services;
using System.Collections.Generic;

namespace SRUnitTests
{
    public class RouteHistoryUnitTest
    {

        [Fact]
        public void TestGetAllRoutes()
        {
            List<string> expected = new List<string>
            {
                "CSULB",
                "2PCH",
                "Turn left. Turn right",
                "Orange"
            };
            List<RouteInformation> list = new List<RouteInformation>();
            string start = "CSULB";
            string end = "2PCH";
            string ins = "Turn left. Turn right";
            string username = "Orange";
            var route = new RouteInformation(start, end, ins, username);
            list.Add(route);
            var testDAO = new RouteHistoryDAO();
            List<RouteInformation> routes = new List<RouteInformation>();
            routes = testDAO.getRouteHistory("Orange");

            Assert.Equal(expected, routes);
        }
    }
}

