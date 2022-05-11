using SafeRide.src.Models;

namespace SafeRide.src.Interfaces
{
    public interface IReportHazardService
    {
        public int Report(Hazard hazard);
        public List<Hazard> GetHazards();
    }
}
