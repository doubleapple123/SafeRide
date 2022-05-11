using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SRUnitTests
{
    public class MockReportHazardService : IReportHazardService
    {
        private List<Hazard> _hazardList;

        public MockReportHazardService()
        {
            _hazardList = new List<Hazard>()
            {
                new Hazard(HazardType.Vehicle, -70.21414, -71.45392, "dshfguj82", "CA", 92602, "Santa Ana", new DateTime(2015, 12, 31, 5, 10, 20), 0),
                new Hazard(HazardType.Accident, -71.21414, -72.45392, "dshfguj82", "CA", 92602, "Irvine", new DateTime(2015, 12, 31, 5, 10, 20), 0),
                new Hazard(HazardType.Closure, -72.21414, -73.45392, "dshfguj82", "CA", 92602, "Tustin", new DateTime(2015, 12, 31, 5, 10, 20), 0),
                new Hazard(HazardType.Obstruction, -73.21414, -74.45392, "dshfguj82", "AZ", 92602, "Flagstaff", new DateTime(2015, 12, 31, 5, 10, 20), 0)
            };
        }

        public List<Hazard> GetHazards()
        {
            return _hazardList;
        }

        public int Report(Hazard hazard)
        {
            _hazardList.Add(hazard);
            return _hazardList.Count;
        }
    }
}
