namespace SafeRide.src.Interfaces;

public interface ISavedRouteDAO
{
    public string GetSavedRoute(string UserName, string RouteName);
    public bool AddSavedRoute(string UserName, string RouteName, string EncodedRoute);
}