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
    public class Step {
        private List<object> intersections;
        private Maneuver maneuver;
        private string name;
        private double duration;
        private double distance;
        private string driving_side;
        private double weight;
        private string mode;
        private object geometry;

        public Step(List<object> intersections, Maneuver maneuver, string name, double duration, double distance, string driving_side, double weight, string mode, object geometry)
        {
            this.intersections = intersections;
            this.maneuver = maneuver;
            this.name = name;
            this.duration = duration;
            this.distance = distance;
            this.driving_side = driving_side;
            this.weight = weight;
            this.driving_side = driving_side;
            this.mode = mode;
            this.geometry = geometry;
        }

        public List<object> Intersections { get => intersections; set => intersections = value; }
        public Maneuver Maneuver { get => maneuver; set => maneuver = value; }
        public string Name { get => name; set => name = value; }
        public double Duration { get => duration; set => duration = value; }
        public double Distance { get => distance; set => distance = value; }
        public string DrivingSide { get => driving_side; set => driving_side = value; }
        public double Weight { get => weight; set => weight = value; }
        public string Mode { get => mode; set => mode = value; }
        public object Geometry { get => geometry; set => geometry = value; }
    }
}