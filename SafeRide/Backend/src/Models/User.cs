namespace SafeRide.src.Models
{
    public class User
    {
        public string UserEmail { get; set; }

        public List<string> Route { get; set; }

        public User(string userEmail, List<string> route)
        {

            UserEmail = userEmail;
            Route = route;
        }
    }
}
