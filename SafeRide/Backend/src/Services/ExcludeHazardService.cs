using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.DataAccess;
using Route = SafeRide.src.Models.Route;

namespace SafeRide.src.Services
{
    public class ExcludeHazardService : IExcludeHazardService
    {
//        private IViewEventDAO _viewEventDAO;
        private IHazardDAO _hazardDAO;
        private Route _route;
        private double _distance;
        private Dictionary<double, double>  _searchCoordinates; 
        private int _searchCount;
        private const double RADIUS_METERS = 80467.12;
        private const int EXCLUSION_LIMIT = 50; // the maximum amount of exclusion arguments for a single directions request set by MapBox API

        /// <summary>
        /// initialize a HazardExclusionService by passing in a
        /// Route object from the directions response
        /// </summary>
        /// <param name="route"></param>
        public ExcludeHazardService(Route route)
        {
            this._hazardDAO = new HazardDAO();
            this._route = route;
            this._distance = route.Distance;
            this._searchCount = 0;
        }

        /// <summary>
        /// finds nearby hazard types by searching radially around each searchCoordinate on the route 
        /// </summary>
        public Dictionary<double, double> FindHazardsNearRoute(List<int> hazards)
        {
            // solution dict to store results
            Dictionary<double, double> results = new Dictionary<double, double>();
            // find the coordinates to target the search radii
            this._searchCoordinates = FindSearchCoordinates();

            while (results.Count < 50)
            {
                // search hazards one at a time
                for (int i = 0; i < hazards.Count; i++)
                {
                    // check for the current hazard type around
                    for (int j = 0; j < _searchCoordinates.Count; j++)
                    {
                        double targetX = _searchCoordinates.ElementAt(i).Key;
                        double targetY = _searchCoordinates.ElementAt(i).Key;
                        Dictionary<double, double> foundCoordinates = _hazardDAO.
            GetByTypeInRadius(hazards[j], targetX, targetY, RADIUS_METERS);
                        // append the results dict with the dict of queried coordinates
                        results.ToList().ForEach(pair => foundCoordinates[pair.Key] = pair.Value);
                    }
                }
            }
            return results;
        }
        
        /// <summary>
        /// uses the pre-defined radius size to find all necessary coordinates that must be searched around to fully span  the route
        /// </summary>
        public Dictionary<double, double> FindSearchCoordinates() {
            Dictionary<double, double> results = new Dictionary<double, double>(); // return variable
            List<Step> routeSteps = _route.Legs[0].Steps; // extract the list of steps taken by the route

           // find all the coordinates between the current step and and the next step that must be searched to cover the distance between them 
            for (int i = 0; i < routeSteps.Count - 1;  i++) {
                // get the distance and coordinates of the current step
                double stepDistance = routeSteps[i].Distance;     
                double startX = routeSteps[i].Maneuver.Location[0];
                double startY = routeSteps[i].Maneuver.Location[1];
                // get the coordinates of the next step
                double endX = routeSteps[i+1].Maneuver.Location[0];
                double endY = routeSteps[i+1].Maneuver.Location[1];

                // automatically add the first and last steps of the route as search coordinates
                if (i == 0 || i == routeSteps.Count - 1)
                {
                    results.Add(startX, startY);
                    _searchCount += 1;
                }
                // for each step in between them, check if still covered under the radius of the last searchCoordinate 
                else
                {
                    if (IsInside(startX, startY, results.ElementAt(_searchCount - 1).Key, results.ElementAt(_searchCount - 1).Value, RADIUS_METERS) == false)
                    {
                        // if not covered by previous radius, add a new searchCoordinate at the current step
                        results.Add(startX, startY);
                        _searchCount += 1;
         
                        // if the step distance is greater than the search radius, then we need to continue adding searh coordinates until reaching the next step
                        if (stepDistance > RADIUS_METERS)
                        {
                             // calculate the number of additional radii needed to span the rest of the leg
                             int reqSearches = (int) Math.Ceiling((stepDistance + RADIUS_METERS - 1) / RADIUS_METERS); // round up to prevent gaps between radii

                            for (int j = 0; j < reqSearches; j++)
                            {
                                // each new searchCoordinate is found by extrapolating a new coordinate that is is a distance of RADIUS_METERS from the last searchCoordinate and in the direction of the last coordinate on this step (i.e., the coordinate where the next step starts) 
                                // formula taken from "https://stackoverflow.com/questions/53404008/how-to-calculate-coordinate-x-meters-away-from-a-point-but-towards-another-in-c" 

                                double ratio = RADIUS_METERS / stepDistance;
                                double prevX = results.ElementAt(_searchCount - j + 1).Key;
                                double prevY = results.ElementAt(_searchCount - j + 1).Value;
                                double diffX = endX - prevX;
                                double diffY = endY - prevY;
                                double nextX = prevX + (ratio * diffX);
                                double nextY = prevY + (ratio * diffY);
                                // add the calculated values as the next search in searchCoordinates
                                results.Add(nextX, nextY);
                                _searchCount += 1;
                            }
                        }
                    }
                }
            }
            return results;
        }


        /// <summary>
        /// given two coordinaets and a search radius, checks if the target coordinate is inside the radius around the center coordinate
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="targetX"></param>
        /// <param name="targetY"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public bool IsInside(double centerX, double centerY, double targetX, double targetY, double radius)
        {  
            double distanceBetween = Math.Sqrt((Math.Pow(centerX - targetX, 2) + Math.Pow(centerY - targetY, 2)));
            return (distanceBetween <= radius);
        }
    }        
}

