using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.DataAccess;
using Newtonsoft.Json;
using Route = SafeRide.src.Models.Route;

namespace SafeRide.src.Services
{
    public class ParseResponseService : IParseResponseService
    {
        private string _jsonResponse;
        private DirectionsResponse _directionsResponse;

        public ParseResponseService()
        {
        }


        public void ParseResponse(string jsonResponse)
        {
            this._jsonResponse = jsonResponse;
            this._directionsResponse = JsonConvert.DeserializeObject<DirectionsResponse>(_jsonResponse);
        }

        public DirectionsResponse GetDirectionsResponse()
        {
            
            return _directionsResponse;
        }


        // helps simplify HazardExclusion by extracting a single route from the response
        public Route GetRoute(int routeNum) {
            return _directionsResponse.Routes[routeNum];
        }

        // helps simplify finding the hazard search radii in the initial route by extracting coordinates from whenever the route takes a step in a new direction
        public Dictionary<double, double> GetStepCoordinates() {
            Dictionary<double, double> results = new Dictionary<double, double>();          
            
            // assume initial route was requested with no additional waypoints beside starting and ending coordinates, so it has only 1 leg
            Route initialRoute = GetRoute(0);
            List<Step> steps = initialRoute.Legs[0].Steps;
            // add each extracted coordinate to the turn coordinatees
            for (int i = 0; i < steps.Count; i++) {
                double stepY = steps[i].Maneuver.Location[0];
                double stepX = steps[i].Maneuver.Location[1];
                results.Add(stepY, stepX);
            }
            return results;
        }
    }
}
