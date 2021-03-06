using SafeRide.src.Models;
using Route = SafeRide.src.Models.Route;

namespace SafeRide.src.Interfaces
{
    public interface IParseResponseService
    {
        public void ParseResponse(string jsonResponse);

        public DirectionsResponse GetDirectionsResponse();

        public Route GetRoute(int routeNum);

        public Dictionary<double, double> GetStepCoordinates();
    }
}
