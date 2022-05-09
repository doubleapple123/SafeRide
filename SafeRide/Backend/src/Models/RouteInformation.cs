namespace Backend.src.Models
{
    public class RouteInformation
    {
        private string startpoint;
        private string endpoint;
        private string geo;
        private string instructions;
        public string StartPoint { get { return startpoint; } set { this.startpoint = value; } }
        public string EndPoint { get { return endpoint; } set { this.endpoint = value; } }
        public string Geo { get { return geo; } set { geo = value; } }
        public string Instructions { get { return instructions; } set { instructions = value; } }


        public RouteInformation()
        {
            StartPoint = "";
            EndPoint = "";
            Geo = "";
            Instructions = "";
        }

        public RouteInformation(string startpoint, string endpoint, string geo, string instructions)
        {
            StartPoint = startpoint;
            EndPoint = endpoint;
            Geo = geo;
            Instructions = instructions;
        }
    }
}
