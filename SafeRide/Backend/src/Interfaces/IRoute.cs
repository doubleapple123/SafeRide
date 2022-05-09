namespace Backend.src.Models
{
    public interface IRoute
    {
        string JsonRequest { get; set; }
        int RouteId { get; set; }
    }
}