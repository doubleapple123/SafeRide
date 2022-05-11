using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using SafeRide.src.Controllers;
using SafeRide.src.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Models;

namespace SRUnitTests
{
    public class ReportHazardControllerTest
    {
        private ReportHazardController _controller;
        private IReportHazardService _service;

        public ReportHazardControllerTest()
        {
            _service = new MockReportHazardService();
            _controller = new ReportHazardController(_service);
        }

        [Fact]
        public void GetHazards_ReturnsOkResult()
        {
            //act
            var okResult = _controller.GetHazards();

            //assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetHazards_ReturnsAllHazards()
        {
            //act
            var expected = new List<Hazard>()
            {
                new Hazard(HazardType.Vehicle, -70.21414, -71.45392, "dshfguj82", "CA", 92602, "Santa Ana", new DateTime(2015, 12, 31, 5, 10, 20), 0),
                new Hazard(HazardType.Accident, -71.21414, -72.45392, "dshfguj82", "CA", 92602, "Irvine", new DateTime(2015, 12, 31, 5, 10, 20), 0),
                new Hazard(HazardType.Closure, -72.21414, -73.45392, "dshfguj82", "CA", 92602, "Tustin", new DateTime(2015, 12, 31, 5, 10, 20), 0),
                new Hazard(HazardType.Obstruction, -73.21414, -74.45392, "dshfguj82", "AZ", 92602, "Flagstaff", new DateTime(2015, 12, 31, 5, 10, 20), 0)
            };
            var okResult = _controller.GetHazards() as OkObjectResult;

            //assert
            var hazards = Assert.IsType<List<Hazard>>(okResult.Value);
            Assert.Equal(expected.Count, hazards.Count);
        }

        [Fact]
        public void Report_ReturnsOkResult()
        {
            //act 
            Hazard h = new Hazard(HazardType.BikeLane, -70.21414, -71.45392, "dshfguj82", "CA", 00000, "San Francisco", new DateTime(2015, 12, 31, 5, 10, 20), 0);
            var okResult = _controller.Report(90, 90, 0);

            //assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
    }
}
