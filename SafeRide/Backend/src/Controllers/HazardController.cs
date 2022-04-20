using System.Text.RegularExpressions;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.Services;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace SafeRide.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api")]
    [Controller]
    [ApiController]
    public class HazardController : ControllerBase
    {
        //private readonly ApplicationUser _user;
        private readonly MapRoute _route;
        private readonly IHazardExclusionService _hazardExclusionService;
        private readonly IParseResponseService _parseResponseService;
                

        public HazardController([FromBody] string jsonResponse) {
            //this._user = user;

            this._parseResponseService = new ParseResponseService(jsonResponse);
            this._hazardExclusionService = new HazardExclusionService(_route);
        }

        /* 
        on frontend: 
        Ajax.BeginForm("Exclude", 
                            new AjaxOptions { UpdateTargetId = "divHazards" }))*/

        [HttpGet]
        [Route("exclude")]
        public IActionResult Exclude([FromBody] List<int> hazards)
        {
            MapRoute firstRoute = _parseResponseService.GetRoute(0);
            var results = _hazardExclusionService.FindHazardsNearRoute(hazards);
            return Ok(new { results });

        }
        ////[HttpPost]
        ////[Route("exclude")]
        //public bool Report(List<int> hazards) {
        //    _hazardExclusionService.FindHazardsNearRoute(hazards);
        //}






    }
}