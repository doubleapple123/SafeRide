namespace Backend.src.Models
{
    public class RouteInformation
    {
        public string startpoint { get; set; }
        public string endpoint { get; set; }
        public string instructions { get; set; }

        public RouteInformation(string startPoint, string endPoint, string Instructions)
        {
            startpoint = startPoint;
            endpoint = endPoint;
            instructions = Instructions;
        }
    }
}
