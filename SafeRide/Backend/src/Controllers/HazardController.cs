using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Interfaces;
using SafeRide.src.Services;
using Route = SafeRide.src.Models.Route;
using Backend.Attributes.AuthorizeAttribute;



namespace SafeRide.Controllers
{
    //  [Route("api/[controller]")]
    //[Microsoft.AspNetCore.Mvc.Route("api")]
    //[Controller]
    [ApiController]
    public class HazardController : ControllerBase
    {
        //private readonly ApplicationUser _user;
        //private Route _route;
        //private readonly IExcludeHazardService _excludeHazardService;
        //private readonly IParseResponseService _parseResponseService;
        private readonly IHazardDAO _hazardDAO;


        //public HazardController(IExcludeHazardService excludeHazardService, IParseResponseService parseResponseService) {
        //this._user = user;
        //this._parseResponseService = parseResponseService;
        //this._excludeHazardService = excludeHazardService;
        //}
        public HazardController(IHazardDAO hazardDAO)
        {
            this._hazardDAO = hazardDAO;
        }

        /* 
        on frontend: 
        Ajax.BeginForm("Exclude", 
                            new AjaxOptions { UpdateTargetId = "divHazards" }))*/

        [Microsoft.AspNetCore.Mvc.HttpGet]              [Backend.Attributes.AuthorizeAttribute.AuthorizeAttribute.ClaimRequirementAttribute("role", "user")]
        [Microsoft.AspNetCore.Mvc.Route("/api/hazard/exclude")]
        // public IActionResult Exclude([FromBody] List<int> hazards, [FromUri] string jsonResponse)
        public async Task<IActionResult> Exclude([FromUri] string request, [FromUri] string hazards)
        {

            // move to services
            // send request to mapbox API
            HttpClient client = new(); // change to static field of controller 
            HttpResponseMessage response = await client.GetAsync(request);

            // extract route from the response
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            IParseResponseService parser = new ParseResponseService();
            parser.ParseResponse(jsonResponse);
            Route firstRoute = parser.GetRoute(0);
            // response.content.ReadStream
            // memory steam->load data->pass bytes to response



            //convert string of hazard type ID's into a list of integers
            var hazardNums = hazards.Split(',').Select(Int32.Parse).ToList();

            // find results
            IExcludeHazardService excludeHazardService = new ExcludeHazardService();
            // non-static instances
            var results = excludeHazardService.FindHazardsNearRoute(hazardNums, firstRoute);

            return Ok(new { results });
        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("/api/hazard/simpleExclude")]
        // public IActionResult Exclude([FromBody] List<int> hazards, [FromUri] string jsonResponse)
        public async Task<IActionResult> SimpleExclude([FromQuery] string hazard)
        {

            // send request to mapbox API
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.mapbox.com/directions/v5/mapbox/driving/-73.832902%2C40.736744%3B-73.834413%2C40.736805?alternatives=true&geometries=geojson&language=en&overview=simplified&steps=true&access_token=pk.eyJ1IjoiY29saW5jcmVhc21hbiIsImEiOiJjbDIxbGhnZ2QxMW1pM2Jwamp4YW42M25zIn0.WJD2zPxATbnf2utML0OOCQ");

            // extract route from the response
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            IParseResponseService parser = new ParseResponseService();
            parser.ParseResponse(jsonResponse);
            Route firstRoute = parser.GetRoute(0);

            //convert string of hazard type ID's into a list of integers
            var hazardNums = hazard.Split(',').Select(Int32.Parse).ToList();

            // find results
            IExcludeHazardService excludeHazardService = new ExcludeHazardService();
            var results = excludeHazardService.FindHazardsNearRoute(hazardNums, firstRoute);

            return Ok(new { results });
        }



        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("/api/hazard/testHazard")]
        public IActionResult GetHazardsTest()
        {
            //var user = JwtDecoder.GetUser(authorization);
            //if (user == null) return Unauthorized
            var coordinates = _hazardDAO.GetAllHazardCoordinates();
            return Ok(new { coordinates });
        }
    }
}