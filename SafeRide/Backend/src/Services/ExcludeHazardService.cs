using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.DataAccess;
using Route = SafeRide.src.Models.Route;
using System.Device.Location;
using CoordinateSharp;

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

        public ExcludeHazardService()
        {

        }
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
                        double targetY = _searchCoordinates.ElementAt(i).Value;
                        double targetX = _searchCoordinates.ElementAt(i).Key;

                        Dictionary<double, double> foundCoordinates = _hazardDAO.
            GetByTypeInRadius(hazards[j], targetY, targetX, RADIUS_METERS);
                        // append the results dict with the dict of queried coordinates
                        results.ToList().ForEach(pair => foundCoordinates[pair.Value] = pair.Key);
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
                double startY = routeSteps[i].Maneuver.Location[0]; // get the  coordinates of the current step               
                double startX = routeSteps[i].Maneuver.Location[1]; 
                double endY = routeSteps[i+1].Maneuver.Location[0];
                double endX = routeSteps[i+1].Maneuver.Location[1];// get the coordinates of the next step

                // calulate the distance between the last search coordinate and the next step coordinate
                 double stepDistance = DistanceBetween(startY, startX, endY, endX);

                // automatically add the first and last steps of the route as search coordinates
                if (i == 0) 
                {
                    results.Add(startY, startX);
                    _searchCount += 1;
                }
                else if (i == routeSteps.Count - 2) {
                    results.Add(endY, endX);
                    _searchCount += 1;
                }

                // for each step in between them, check if still covered under the radius of the last searchCoordinate 
                else
                {
                    if (IsInside(startY, startX, results.ElementAt(_searchCount - 1).Value, results.ElementAt(_searchCount - 1).Key, RADIUS_METERS) == false)
                    {
                        // if not covered by previous radius, add a new searchCoordinate at the current step
                        results.Add(startY, startX);
                        _searchCount += 1;

                        // if the distance to the next step  is greater than the radius, we need to continue adding searh coordinates until reaching the next step

                        if (stepDistance > RADIUS_METERS)
                        {
                             // calculate the number of additional radii needed to span the rest of the leg
                             int reqSearches = (int) Math.Ceiling((stepDistance + RADIUS_METERS - 1) / RADIUS_METERS); // round up to prevent gaps between radii

                            for (int j = 0; j < reqSearches; j++)
                            {
                                // each new searchCoordinate is found by extrapolating a new coordinate that is is a distance of RADIUS_METERS from the last searchCoordinate and in the direction of the last coordinate on this step (i.e., the coordinate where the next step starts) 
                                // formula taken from "https://stackoverflow.com/questions/53404008/how-to-calculate-coordinate-x-meters-away-from-a-point-but-towards-another-in-c" 

                                double ratio = RADIUS_METERS / stepDistance;
                                
                                double prevX = results.ElementAt(_searchCount - j + 1).Value;
                                double prevY =  results.ElementAt(_searchCount - j + 1).Key;
                                double diffY = endY - prevY;
                                double diffX = endX - prevX;
                        
                                double nextY = prevY + (ratio * diffY);
                                double nextX = prevX + (ratio * diffX);   // add the calculated values as the next search in searchCoordinates
                                results.Add(nextY, nextX);
                                _searchCount += 1;
                            }
                        }
                    }
                }
            }
            return results;
        }

        public double DistanceBetween(double y1, double x1, double y2, double x2) {
            Coordinate first = new Coordinate(y1, x1);
            Coordinate last = new Coordinate(y2, x2);
            Distance distance = new Distance(first, last);
            double distanceInMeters = (double) distance.Meters;
            return distanceInMeters;

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

            Coordinate center = new Coordinate(centerY, centerX);
            Coordinate target = new Coordinate(targetY, targetX);
            //center.FormatOptions.Format = CoordinateFormatType.Decimal_Degree;
            //target.FormatOptions.Format = CoordinateFormatType.Decimal_Degree;
            Distance distance = new Distance(center, target);
            double distanceInMeters = (double) distance.Meters;
            //double distanceBetween = Math.Sqrt((Math.Pow(centerX - targetX, 2) + Math.Pow(centerY - targetY, 2)));
            Console.WriteLine("Centre latitude: " + center.Latitude.DecimalDegree);
            Console.WriteLine("Centre longitude: " + center.Longitude.DecimalDegree);
            Console.WriteLine(distanceInMeters);
            return (distanceInMeters <= radius);
        }
    }        
}

