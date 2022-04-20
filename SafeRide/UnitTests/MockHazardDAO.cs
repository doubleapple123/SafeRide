using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SRUnitTests
{
    internal class MockHazardDAOSuccesses : IHazardDAO
    {
        public Dictionary<double, double> FindHazardInRadius(HazardType type, double searchX, double searchY, double radius)
        {
            throw new NotImplementedException();
        }

        public List<Hazard> GetAllValidHazards()
        {
            List<Hazard> hazards = new List<Hazard>();
            Hazard h1 = new Hazard(HazardType.Vehicle, -70.21414, -71.45392, "dshfguj82", "CA", 92602, "Santa Ana", new DateTime(2022, 03, 07, 22, 30, 5), 0);
            Hazard h2 = new Hazard(HazardType.Accident, -71.21414, -72.45392, "dshfguj82", "CA", 92602, "Irvine", new DateTime(2022, 03, 07, 23, 35, 5), 0);
            Hazard h3 = new Hazard(HazardType.Closure, -72.21414, -73.45392, "dshfguj82", "CA", 92602, "Tustin", new DateTime(2022, 03, 07, 24, 5, 5), 0);
            Hazard h4 = new Hazard(HazardType.Obstruction, -73.21414, -74.45392, "dshfguj82", "AZ", 92602, "Flagstaff", new DateTime(2022, 03, 07, 20, 17, 5), 0);
            hazards.Add(h1);
            hazards.Add(h2);
            hazards.Add(h3);
            hazards.Add(h4);
            return hazards;
        }

        public int Report(Hazard hazard)
        {
            return 1;
        }
    }
}
