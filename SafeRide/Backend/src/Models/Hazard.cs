using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeRide.src.Models;


namespace SafeRide.src.Models
{

    /// <summary>
    /// Enum which represents type of hazard.
    /// </summary>
    public enum HazardType
    {
        Accident,
        Obstruction,
        BikeLane,
        Vehicle,
        Closure
    }

    public class Hazard
    {
        private HazardType _type;
        public HazardType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        private DateTime _timeReported = DateTime.UnixEpoch;
        public DateTime TimeReported
        {
            get { return _timeReported; }
            set { _timeReported = value; }
        }

        private string _state;
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        private int _zip;
        public int Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private int _expired;
        public int Expired
        {
            get { return _expired; }
            set { _expired = value; }
        }

        private string _reportedBy;
        public string ReportedBy
        {
            get { return _reportedBy; }
            set { _reportedBy = value; }
        }

        /// <summary>
        /// Ctor for hazard object
        /// </summary>
        /// <param name="type">HazardType enum representing category of hazard</param>
        /// <param name="hazardX">double reprenting latitude</param>
        /// <param name="hazardY">double representing longitude</param>
        /// <param name="reportedBy">string of hash of user who reported the hazard</param>
        /// <param name="state">state where hazard is located</param>
        /// <param name="zip">zip code where hazard is located</param>
        /// <param name="city">city where hazard is located</param>
        /// <param name="dateReported">date and time when hazard was reported</param>
        /// <param name="expired">if hazard is expired or not. made int to make writing to underlying database easier (no bools)</param>
        public Hazard(HazardType type, double hazardX, double hazardY, string reportedBy, string state, int zip, string city, DateTime dateReported, int expired)
        {
            _type = type;
            _latitude = hazardX;
            _longitude = hazardY;
            _reportedBy = reportedBy;
            _timeReported = dateReported;
            _state = state;
            _zip = zip;
            _city = city;
            _expired = expired;
        }

        /// <summary>
        /// == operator for testing equality of hazard objects
        /// </summary>
        /// <param name="h1">first hazard</param>
        /// <param name="h2">second hazard</param>
        /// <returns>true if objects are same by value, else false</returns>
        public static bool operator == (Hazard h1, Hazard h2)
        {
            if ((object)h1 == null)
                return (object)h2 == null;

            return h1.Equals(h2);
        }

        /// <summary>
        /// != operator for testing inequality of hazards
        /// </summary>
        /// <param name="h1">first hazard</param>
        /// <param name="h2">second hazard</param>
        /// <returns>true if objects are different by value, else false</returns>
        public static bool operator != (Hazard h1, Hazard h2)
        {
            return !(h1 == h2);
        }

        /// <summary>
        /// override for Equals method of object
        /// </summary>
        /// <param name="obj">object to be compared</param>
        /// <returns>true if objects are same type and equal by value, else false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                    return false;

            var h2 = (Hazard)obj;
            return _type == h2.Type && _latitude == h2.Latitude && _longitude == h2.Longitude && _reportedBy == h2.ReportedBy && _timeReported == h2.TimeReported && _state == h2.State && _city == h2.City && _zip == h2.Zip && _expired == h2.Expired;
        }

        /// <summary>
        /// override for GetHashCode operator of object
        /// </summary>
        /// <returns>hash code of hazard object</returns>
        public override int GetHashCode()
        {
            return _type.GetHashCode() ^ _latitude.GetHashCode() ^  _longitude.GetHashCode() ^ _reportedBy.GetHashCode() ^ _timeReported.GetHashCode() ^ _state.GetHashCode() ^ _city.GetHashCode() ^ _zip.GetHashCode() ^ _expired.GetHashCode();
        }
    }
}