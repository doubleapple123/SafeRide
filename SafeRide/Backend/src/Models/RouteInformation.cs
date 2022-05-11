namespace SafeRide.src.Models
{
    public class RouteInformation
    {
        public string startpoint { get; set; }
        public string endpoint { get; set; }
        public string instructions { get; set; }
        public string username { get; set; }
        public RouteInformation(string startPoint, string endPoint, string Instructions, string UserName)
        {
            startpoint = startPoint;
            endpoint = endPoint;
            instructions = Instructions;
            username = UserName;
        }
    }
}
