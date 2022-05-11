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
        private IRouteInformationDAO _dao;
        private ISaveRouteService _srs;
        private readonly ITestOutputHelper output;

        public RouteHistoryUnitTest()
        {
            _dao = new RouteHistoryDAO();
            _srs = new SavedRouteService(_dao);
        }

        [Fact]
        public void TestGetAllRoutes()
        {
            List<RouteInformation> list = new List<RouteInformation>();
            string startpoint = "CSULB";
            string endpoint = "2PCH";
            string ins = "Turn left. Turn right";
            string username = "Orange";

            list.Add("CLUB", "2PCH", "Turn left > Turn right", "Orange");

        }
    }
}

