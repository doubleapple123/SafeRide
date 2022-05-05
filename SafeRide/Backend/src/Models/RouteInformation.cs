namespace Backend.src.Models
{
    public class RouteInformation
    {
        private string startpoint;
        private string endpoint;
        private List<List<String>> geo;
        private List<String> instructions;
       

        public string StartPoint { get { return startpoint; } set { this.startpoint = value; } }
        public string EndPoint { get { return endpoint; } set { this.endpoint = value; } }
        public List<List<String>> Geo { get { return geo; } set { geo = value; } }
        public List<String> Instructions { get { return instructions; } set { instructions = value; } }


        public RouteInformation()
        {
            StartPoint = "";
            EndPoint = "";
            Geo = new List<List<String>>();
            Instructions = new List<String>();
        }

        public RouteInformation(string startpoint, string endpoint, List<List<String>> geo, List<String> instructions)
        {
            StartPoint = startpoint;
            EndPoint = endpoint;
            Geo = geo;
            Instructions = instructions;
        }
    }
}
