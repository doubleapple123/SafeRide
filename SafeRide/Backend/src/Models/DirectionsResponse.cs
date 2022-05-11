using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeRide.src.Models
{
    /// <summary>
    /// Stores all serialized data from API Directions response
    /// </summary>
    public class DirectionsResponse 
    {
        private List<Route> routes;
        private List<object> waypoints;
        private string code;
        private string uuid;


        public DirectionsResponse(List<Route> routes, List<object> waypoints, string code, string uuid)
        {
            this.routes = routes;
            this.waypoints = waypoints;
            this.code = code;
            this.uuid = uuid;
        }

        public List<Route> Routes { get => routes; set => routes = value; }
        public List<object> Waypoints { get => waypoints; set => waypoints = value; }
        public string Code { get => code; set => code = value; }
        public string Uuid { get => uuid; set => uuid = value; }
    }
}