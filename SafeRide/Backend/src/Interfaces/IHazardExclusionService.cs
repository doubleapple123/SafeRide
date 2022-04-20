﻿using SafeRide.src.Models;

namespace SafeRide.src.Interfaces
{
    public interface IHazardExclusionService
    {
        public Dictionary<double, double> FindHazardsNearRoute(List<HazardType> hazards);
        public Dictionary<double, double> FindSearchCoordinates();
        public Dictionary<double, double> RadialSearch(Dictionary<double, double> coordinates, HazardType type);
    }
}
