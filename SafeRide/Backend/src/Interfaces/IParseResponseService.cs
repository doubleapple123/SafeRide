using SafeRide.src.Models;

namespace SafeRide.src.Interfaces
{
    public interface IParseResponseService
    {
        public MapRoute GetRoute(int routeNum);

        public Dictionary<double, double> GetStepCoordinates();
    }
}
