using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeRide.src.Models;
using SafeRide.src.Interfaces;

namespace SRUnitTests.Mocks
{
    public class MockReportHazardServiceEmpty : IReportHazardService
    {
        private List<Hazard>? _hazardList;

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
