namespace SafeRide.src.Models
{
    public class User
    {
        public string UserEmail { get; set; }
        public int RouteId { get; set; }
        public string Route { get; set; }

        public User(string userEmail, int routeId, string route)
        {

            UserEmail = userEmail;
            RouteId = routeId;
            Route = route;
        }
    }
}
