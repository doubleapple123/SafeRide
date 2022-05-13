
using System.Collections.Generic;
using SafeRide.src.DataAccess;
using SafeRide.src.Managers;
using SafeRide.src.Models;
using SafeRide.src.Interfaces;
using SafeRide.src.Services;
using Xunit;

namespace SRUnitTests
{
    public class SaveARouteTest
    {
        [Fact]
        public void GetSavedRoutesTest()
        {
            List<string> expected = new List<string>
            {
                "routeurl1",
                "routeurl2",
                "routeurl3",
                "routeurl4",
                "routeurl5",
                "routeurl6",
                "routeurl7",
                "routeurl8",
                "routeurl9"
            };


            var testDAO = new SaveARouteDAO();
            List<string> results = new List<string>();
            results = testDAO.GetSavedRoutes("testUser@gmail.com", "testUserTempRoute");

            Assert.Equal(expected, results);


        }
        [Fact]
        public void AddSavedRouteTest()
        {
            var testDAO = new SaveARouteDAO();
            int results = testDAO.AddSavedRoute("testUser@gmail.com", 4, "routeurddlTEST", "testUserRoute");
            int expected = 1;


            Assert.Equal(expected, results);
        }
        [Fact]
        public void DeleteASavedRoute()
        {

            var testDAO = new SaveARouteDAO();
            
            int results = testDAO.DeleteSavedRoute("testUser@gmail.com", 4, "testUserRoute");
            int expected = 1;

            Assert.Equal(expected, results);
        }


     

        ////<Summary>
        ////Test to see if added routes from a temp route table to a 
        ////user table. User will view Recent route table, and add a specific route.
        ////</Summary>
        //[Fact]
        //public void SaveRouteThenViewRoute()
        //{
        //    var results = new List<string> { "routeurl4" };

        //    var testDAO = new SavedRoutesDAO();
        //    var userRoutesList = testDAO.GetSavedRoutes("testUser@gmail.com", "testUserTempRoute");
        //    var chosenRoute = userRoutesList[3];
        //    testDAO.AddSavedRoute("testUser@gmail.com", 5, chosenRoute, "testUserRoute");
        //    var actual = testDAO.GetSavedRoutes("testUser@Gmail.com", "testUserRoute");

        //    testDAO.DeleteSavedRoute("testUser@gmail.com", 5, "testUserRoute");

        //    Assert.Equal(results, actual);


        //}



    }
}