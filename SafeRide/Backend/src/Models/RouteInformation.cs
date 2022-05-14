using SafeRide.src.Interfaces;

namespace SafeRide.src.Models
{
    public class RouteInformation : IRouteInformation 
    {
        /*public string startpoint { get; set; }
        public string endpoint { get; set; }
        public string instructions { get; set; }
        public string username { get; set; }
        public RouteInformation(string startPoint, string endPoint, string Instructions, string UserName)
        {
            startpoint = startPoint;
            endpoint = endPoint;
            instructions = Instructions;
            username = UserName;
        }*/

        public int RouteId { get ; set; }
        public string StringUrl { get; set; } = string.Empty;

        public RouteInformation(int id, string url)
        {
            RouteId = id;
            StringUrl = url;
        }
    }
}
