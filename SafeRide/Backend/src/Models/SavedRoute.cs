namespace SafeRide.src.Models;

public class SavedRoute
{
    public string RouteName { get; set; }
    public string EncodedRoute { get; set; }
    
    public SavedRoute(string routeName, string encodedRoute)
    {
        RouteName = routeName;
        EncodedRoute = encodedRoute;
    }
}