using System.Web.Helpers;
using SafeRide.src.DataAccess;
using SafeRide.src.Interfaces;

namespace Backend.Services;

public class SavedRouteService
{
    private ISavedRouteDAO _IsavedRouteDao;
    public SavedRouteService(ISavedRouteDAO savedRouteDao)
    {
        _IsavedRouteDao = savedRouteDao;
    }

    public string GetSavedRoute(string UserName, string RouteName)
    {
        return _IsavedRouteDao.GetSavedRoute(UserName, RouteName);
    }

    public bool AddSavedRoute(string UserName, string RouteName, string EncodedRoute)
    {
        return _IsavedRouteDao.AddSavedRoute(UserName, RouteName, EncodedRoute);
    }

    public string DecodeRoute(string encodedRoute)
    {
        return Json.Decode<string>(encodedRoute);
    }
}