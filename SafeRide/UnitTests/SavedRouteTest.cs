
using System.Collections.Generic;
using SafeRide.src.DataAccess;
using SafeRide.src.Managers;
using SafeRide.src.Models;
using Xunit;

namespace SRUnitTests
{
    public class SavedRouteTest
    {
        [Fact]
        public void GetSavedRouteTest()
        {
            
            var testDAO = new SavedRoutesDAO();
            List<string> results = new List<string>;
            results = testDAO.GetRecentRoutes("1");
            List<string> expected = new List<string> { "routeurl", "routeurl2" };
            

            Assert.Equal(results,expected);

               
        }

        

    }
}