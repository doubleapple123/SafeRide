
namespace Backend.src.Services
{
    public interface ISavedRouteService
    {
        int AddSavedRoute(string UserID, int routeId, string route);
        Task<string> GetRouteData(string startLocation, string endLocation);
    }
}