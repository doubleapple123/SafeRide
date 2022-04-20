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
        }

        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
        }
        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
        }

        private DateTime _timeReported;
        public DateTime TimeReported
        {
            get { return _timeReported; }
        }

        private string _state;
        public string State
        {
            get { return _state; }
        }

        private int _zip;
        public int Zip
        {
            get { return _zip; }
        }

        private string _city;
        public string City
        {
            get { return _city; }
        }

        private int _expired;
        public int Expired
        {
            get { return _expired; }
        }

        private string _reportedBy;
        public string ReportedBy
        {
            get { return _reportedBy; }
        }

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

        public static bool operator == (Hazard h1, Hazard h2)
        {
            if ((object)h1 == null)
                return (object)h2 == null;

            return h1.Equals(h2);
        }

        public static bool operator != (Hazard h1, Hazard h2)
        {
            return !(h1 == h2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                    return false;

            var h2 = (Hazard)obj;
            return _type == h2.Type && _latitude == h2.Latitude && _longitude == h2.Longitude && _reportedBy == h2.ReportedBy && _timeReported == h2.TimeReported && _state == h2.State && _city == h2.City && _zip == h2.Zip && _expired == h2.Expired;
        }

        public override int GetHashCode()
        {
            return _type.GetHashCode() ^ _latitude.GetHashCode() ^  _longitude.GetHashCode() ^ _reportedBy.GetHashCode() ^ _timeReported.GetHashCode() ^ _state.GetHashCode() ^ _city.GetHashCode() ^ _zip.GetHashCode() ^ _expired.GetHashCode();
        }
    }
}