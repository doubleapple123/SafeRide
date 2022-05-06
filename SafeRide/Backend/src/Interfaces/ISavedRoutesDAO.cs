using SafeRide.src.Models;

namespace SafeRide.src.Interfaces
{
    public interface ISavedRoutesDAO
    {

        public List<SavedRoutes> GetRouteHistory(string userName);
        

    }
}
