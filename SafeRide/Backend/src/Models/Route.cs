namespace Backend.src.Models
{
    public class Route : IRoute
    {
        public int RouteId { get; set; }
        public string JsonRequest { get; set; } = string.Empty;
    }
}
