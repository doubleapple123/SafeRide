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
    public class Route
    {
        private string weight_name;
        private double weight;
        private double duration;
        private double distance;
        private List<Leg> legs;
        private object geometry;

        public Route(string weightName, double weight, double duration, double distance, List<Leg> legs, object geometry) {
            this.weight_name = weightName;
            this.weight = weight;
            this.duration = duration;
            this.distance = distance;
            this.legs = legs;
            this.geometry = geometry;
        }

        public string WeightName { get => weight_name; set => weight_name = value; }
        public double Weight { get => weight; set => weight = value; }
        public double Duration { get => duration; set => duration = value; }
        public double Distance { get => distance; set => distance = value; }
        public List<Leg> Legs { get => legs; set => legs = value; }
        public object Geometry { get => geometry; set => geometry = value; }
    }
}