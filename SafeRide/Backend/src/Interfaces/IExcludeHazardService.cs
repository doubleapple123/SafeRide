using SafeRide.src.Models;
using Route = SafeRide.src.Models.Route;

namespace SafeRide.src.Interfaces
{
    public interface IExcludeHazardService
    {
        public Dictionary<double, double> FindHazardsNearRoute(List<int> hazards, Route route);

        public Dictionary<double, double> FindSearchCoordinates(Route route);

        bool IsInside(double centerX, double centerY, double targetX, double targetY, double radius);

    }
}
