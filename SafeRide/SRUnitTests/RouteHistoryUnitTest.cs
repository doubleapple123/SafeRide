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
            List<string> expected = new List<string>()
            {
                "url1",
                "url2",
                "url3"
            };

            var testDAO = new RouteHistoryDAO();
            List<string> results = new List<string>();
            results = testDAO.getRouteHistory("Apple", "appleRecentSearches");

            Assert.Equal(expected, results);
        }
    }
}

