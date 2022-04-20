using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SafeRide.src.Models;
using SafeRide.src.DataAccess;
using SafeRide.src.Interfaces;

namespace SRUnitTests
{
    public class HazardDAOTest
    {
        [Fact]
        public void ValidReport()
        {
            //arrange
            Hazard hazard = new Hazard(HazardType.Accident, -70, -71, "testHash", "CA", 92686, "Mission Viejo", DateTime.Now, 0);
            IHazardDAO hazardDAO = new HazardDAO();

            //act
            int status = hazardDAO.Report(hazard);

            //assert
            Assert.Equal(1, status);
        }

        //[Fact]
        //public void ValidGetAllHazards()
        //{

        //}
    }
}
