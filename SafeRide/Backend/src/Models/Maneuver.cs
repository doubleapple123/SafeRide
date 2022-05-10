using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeRide.src.Models
{
    /// <summary>
    /// Holds data from API response grouped by maneuvers; 
    /// each maneuver represents a part of the route where the direction has changed 
    /// </summary>
    public class Maneuver {


        private string type;
        private string instruction;
        private string modifier;
        private double bearing_after;
        private double bearing_before;
        private List<double> location;

        public Maneuver(string type, string instruction, string modifier, double bearing_after, double bearing_before, List<double> location)
        {
            this.type = type;
            this.instruction = instruction;
            this.modifier = modifier;
            this.bearing_after = bearing_after;
            this.bearing_before = bearing_before;
            this.location = location;
        }

        public string Type { get => type; set => type = value; }
        public string Instruction { get => instruction; set => instruction = value; }
        public string Modifier { get => modifier; set => modifier = value; }
        public double BearingAfter { get => bearing_after; set => bearing_after = value; }
        public double BearingBefore { get => bearing_before; set => bearing_before = value; }
        public List<double> Location { get => location; set => location = value; }
    }
}