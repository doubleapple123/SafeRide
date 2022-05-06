namespace SafeRide.src.Models
{
    public class SavedRoute
    {
        public string UserId { get; set; }
        public int RouteId { get; set; }
        public string Route { get; set; }

        public SavedRoute(string userId, int routeId, string route)
        {

            UserId = userId;
            RouteId = routeId; 
            Route = route;

        }
    }
}
