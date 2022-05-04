namespace SafeRide.src.Models;

public class SavedRoute
{
    public string UserName { get; set; }
    public string EncodedRoute { get; set; }
    
    public SavedRoute(string userName, string encodedRoute)
    {
        UserName = userName;
        EncodedRoute = encodedRoute;
    }
}