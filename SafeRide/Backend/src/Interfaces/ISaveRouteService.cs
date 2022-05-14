using SafeRide.src.Models;

namespace SafeRide.src.Interfaces
{
    public interface ISaveRouteService
    {
        public List<string> GetAllRoutes(string UserName);
    }
}
