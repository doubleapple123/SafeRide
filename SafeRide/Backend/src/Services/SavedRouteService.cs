using System.Text.Json;
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
        return _IsavedRouteDao.GetSavedRoute(UserName, RouteName);
    }

    public bool AddSavedRoute(string UserName, string RouteName, string encodedJson)
    {
        return _IsavedRouteDao.AddSavedRoute(UserName, RouteName, encodedJson);
    }

    public List<SavedRoute> GetAllRoutes(string UserName)
    {
        var routeList = _IsavedRouteDao.GetAllSavedRoutes(UserName);
        // routeList.ForEach((item) => item.EncodedRoute = DecodeRoute(item.EncodedRoute));
        return routeList;
    }
    public string EncodeRoute(string rawJson)
    {
        return JsonSerializer.Serialize(rawJson);
    }
}