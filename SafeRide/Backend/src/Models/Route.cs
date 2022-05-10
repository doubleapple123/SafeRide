namespace Backend.src.Models
{
    public class Route : IRoute
    {
        public int RouteId { get; set; }
        public string UrlString { get; set; } = string.Empty;


        public Route(int routeId, string urlString)
        { 
            RouteId = routeId;
            UrlString = urlString;
        }
    }
}
