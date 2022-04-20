using System.Text.RegularExpressions;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using SafeRide.src.Services;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using Route = SafeRide.src.Models.Route;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace SafeRide.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api")]
    [ApiController]
    public class HazardController : ControllerBase
    {
        //private readonly ApplicationUser _user;
        private readonly Route _route;
        private readonly IExcludeHazardService _hExcludeHazardService;
        private readonly IParseResponseService _parseResponseService;
                

        public HazardController([FromBody] string jsonResponse) {
            //this._user = user;

            this._parseResponseService = new ParseResponseService(jsonResponse);
            this._hExcludeHazardService = new ExcludeHazardService(_route);
        }

        /* 
        on frontend: 
        Ajax.BeginForm("Exclude", 
                            new AjaxOptions { UpdateTargetId = "divHazards" }))*/

        [HttpGet]
        [Route("exclude")]
        public IActionResult Exclude([FromBody] List<int> hazards)
        {
            Route firstRoute = _parseResponseService.GetRoute(0);
            var results = _hExcludeHazardService.FindHazardsNearRoute(hazards);
            return Ok(new { results });

        }
        ////[HttpPost]
        ////[Route("exclude")]
        //public bool Report(List<int> hazards) {
        //    _hExcludeHazardService.FindHazardsNearRoute(hazards);
        //}
    }
}