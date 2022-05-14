using Xunit;
using Xunit.Abstractions;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.DataAccess;
using SafeRide.src.Services;
using System.Collections.Generic;
using System;

namespace SRUnitTests
{
    public class RouteHistoryUnitTest
    {

        [Fact]
        public void TestGetAllRoutes()
        {
            List<RouteInformation> expected = new List<RouteInformation>();
            List<RouteInformation> list = new List<RouteInformation>();
            string start = "CSULB";
            string end = "2PCH";
            string ins = "Turn left. Turn right";
            string username = "Orange";
            expected = RouteInformation(start, end, ins, username);
            var testDAO = new RouteHistoryDAO();
            List<RouteInformation> routes = new List<RouteInformation>();
            routes = testDAO.getRouteHistory("Orange");

            Assert.Equal(expected, routes);
        }

        private List<RouteInformation> RouteInformation(string start, string end, string ins, string username)
        {
            throw new NotImplementedException();
        }
    }
}

