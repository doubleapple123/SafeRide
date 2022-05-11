using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeRide.src.Interfaces;
using SafeRide.src.Services;
using SafeRide.src.Models;
using Xunit;

namespace SRUnitTests
{
    public class ReportHazardServiceTest
    {
        private IHazardDAO _dao;
        private IReportHazardService _service;

        public ReportHazardServiceTest()
        {
            _dao = new MockHazardDAOSuccess();
            _service = new ReportHazardService(_dao);
        }

        [Fact]
        public void GetHazards_ReturnsCorrectNumberHazards()
        {
            //act
            var expected = new List<Hazard>()
            {
                new Hazard(HazardType.Vehicle, -70.21414, -71.45392, "dshfguj82", "CA", 92602, "Santa Ana", new DateTime(2015, 12, 31, 5, 10, 20), 0),
                new Hazard(HazardType.Accident, -71.21414, -72.45392, "dshfguj82", "CA", 92602, "Irvine", new DateTime(2015, 12, 31, 5, 10, 20), 0),
                new Hazard(HazardType.Closure, -72.21414, -73.45392, "dshfguj82", "CA", 92602, "Tustin", new DateTime(2015, 12, 31, 5, 10, 20), 0),
                new Hazard(HazardType.Obstruction, -73.21414, -74.45392, "dshfguj82", "AZ", 92602, "Flagstaff", new DateTime(2015, 12, 31, 5, 10, 20), 0)
            };
            var actual = _service.GetHazards();

            //assert
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void GetHazards_ReturnsCorrectHazards()
        {
            //act
            var expected = new List<Hazard>()
            {
                new Hazard(HazardType.Vehicle, -70.21414, -71.45392, "dshfguj82", "CA", 92602, "Santa Ana", new DateTime(2015, 12, 31, 5, 10, 20), 0),
                new Hazard(HazardType.Accident, -71.21414, -72.45392, "dshfguj82", "CA", 92602, "Irvine", new DateTime(2015, 12, 31, 5, 10, 20), 0),
                new Hazard(HazardType.Closure, -72.21414, -73.45392, "dshfguj82", "CA", 92602, "Tustin", new DateTime(2015, 12, 31, 5, 10, 20), 0),
                new Hazard(HazardType.Obstruction, -73.21414, -74.45392, "dshfguj82", "AZ", 92602, "Flagstaff", new DateTime(2015, 12, 31, 5, 10, 20), 0)
            };
            var actual = _service.GetHazards();

            //assert
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}
