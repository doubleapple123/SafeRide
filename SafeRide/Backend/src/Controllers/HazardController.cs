using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Interfaces;
using SafeRide.src.Services;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using Route = SafeRide.src.Models.Route;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace SafeRide.Controllers
{
  //  [Route("api/[controller]")]
    //[Microsoft.AspNetCore.Mvc.Route("api")]
    //[Controller]
    [ApiController]
    public class HazardController : ControllerBase
    {
        //private readonly ApplicationUser _user;
       // private readonly Route _route;
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

        //[HttpPost]
        //[Route("/api/hazard/exclude")]
        //public IActionResult Exclude([FromBody] List<int> hazards, string jsonResponse)
        //{
        //    _parseResponseService.ParseResponse(jsonResponse);
        //    Route firstRoute = _parseResponseService.GetRoute(0);

        //    var results =_excludeHazardService.FindHazardsNearRoute(hazards);
        //    return Ok(new { results });

        //}



        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("/api/hazard/testHazard")]
        public IActionResult GetHazards()
        {
            //var user = JwtDecoder.GetUser(authorization);
            //if (user == null) return Unauthorized();

            var coordinates = _hazardDAO.GetAllHazardCoordinates();


            return Ok(new { coordinates });

        }

            ////[HttpPost]
            ////[Route("exclude")]
            //public bool Report(List<int> hazards) {
            //    _hExcludeHazardService.FindHazardsNearRoute(hazards);
            //}
        }
}