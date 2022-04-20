using SafeRide.src.DataAccess;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

namespace SafeRide.src.Services
{
    public class ReportHazardService : IReportHazardService
    {
        private IHazardDAO _hazardDAO;

        public ReportHazardService(IHazardDAO hazardDAO)
        {
            _hazardDAO = hazardDAO;
        }

        public int Report(Hazard hazard)
        {
            return _hazardDAO.Report(hazard);
        }

        //public Task GetHazards();
    }
}
