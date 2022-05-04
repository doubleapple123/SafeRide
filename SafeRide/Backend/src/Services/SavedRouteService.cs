using System.Web.Helpers;
using SafeRide.src.DataAccess;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;

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
        return DecodeRoute(_IsavedRouteDao.GetSavedRoute(UserName, RouteName));
    }

    public bool AddSavedRoute(string UserName, string RouteName, string rawJson)
    {
        return _IsavedRouteDao.AddSavedRoute(UserName, RouteName, EncodeRoute(rawJson));
    }

    public List<SavedRoute> GetAllRoutes(string UserName)
    {
        var routeList = _IsavedRouteDao.GetAllSavedRoutes(UserName);
        routeList.ForEach((item) => item.EncodedRoute = DecodeRoute(item.EncodedRoute));
        return routeList;
    }

    public string DecodeRoute(string encodedRoute)
    {
        return Json.Decode<string>(encodedRoute);
    }

    public string EncodeRoute(string rawJson)
    {
        return Json.Encode(rawJson);
    }
}