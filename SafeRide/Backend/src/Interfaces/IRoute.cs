namespace Backend.src.Models
{
    public interface IRoute
    {
        string UrlString { get; set; }
        int RouteId { get; set; }
    }
}