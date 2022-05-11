using SafeRide.src.DataAccess;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SafeRide.src.Services
{

    /// <summary>
    /// Class which handles logic for actions relating to hazard reporting feature
    /// </summary>
    public class ReportHazardService : IReportHazardService
    {
        private IHazardDAO _hazardDAO;

        /// <summary>
        /// Ctor for ReportHazard service. Depends on data acccess layer
        /// </summary>
        /// <param name="hazardDAO">instance of data access object to use</param>
        public ReportHazardService(IHazardDAO hazardDAO)
        {
            _hazardDAO = hazardDAO;
        }

        /// <summary>
        /// Gets all valid hazards
        /// </summary>
        /// <returns>List of non-expired hazards</returns>
        public List<Hazard> GetHazards()
        {
            return _hazardDAO.GetAllValidHazards();
            //add error checking for all failure cases
        }

        /// <summary>
        /// Reports hazard
        /// </summary>
        /// <param name="hazard">hazard to report</param>
        /// <returns>integer representing number of hazards reported (should be 1 per method call)</returns>
        public int Report(Hazard hazard)
        {
            if (hazard.TimeReported == DateTime.UnixEpoch)
                hazard.TimeReported = DateTime.Now;
            return _hazardDAO.Report(hazard);
        }
    }
}
