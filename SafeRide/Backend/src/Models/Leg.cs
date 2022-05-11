using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeRide.src.Models
{
    /// <summary>
    /// Holds data for the Route object from the API response; 
    /// each maneuver represents a part of the route where the direction has changed 
    /// </summary>
    public class Leg
    {
        private List<object> via_waypoints;
        private List<object> admins;
        private double weight;
        private double duration;
        private List<Step> steps;
        private double distance;
        private string summary;

        public Leg(List<object> via_waypoints, List<object> admins, double weight, double duration, List<Step> steps, double distance, string summary)
        {
            this.via_waypoints = via_waypoints;
            this.admins = admins;
            this.weight = weight;
            this.duration = duration;
            this.steps = steps;
            this.distance = distance;
            this.summary = summary;
        }

        public List<object> ViaWaypoints { get => via_waypoints; set => via_waypoints = value; }
        public List<object> Admins { get => admins; set => admins = value; }
        public double Weight { get => weight; set => weight = value; }
        public double Duration { get => duration; set => duration = value; }
        public List<Step> Steps { get => steps; set => steps = value; }
        public double Distance { get => distance; set => distance = value; }
        public string Summary { get => summary; set => summary = value; }
    }
}

