using System.Text.RegularExpressions;
using System.Web.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace SafeRide.src.Controllers
{
    [Route("api/hazards")]
    [ApiController]
    [Produces("application/json")]
    public class HazardController : ControllerBase
    {
      //  private readonly IHazardExclusionService _hazardExclusionService;
        private readonly IReportHazardService _reportHazardService;

        public HazardController(/*IHazardExclusionService hazardExclusionService, */IReportHazardService reportHazardService)
        {
           // _hazardExclusionService = hazardExclusionService;
            _reportHazardService = reportHazardService;
        }

        [HttpPost]
        [DisableCors]
        [Route("report")]
        public IActionResult Report(Hazard h)
        {
            Hazard hazard = new Hazard(HazardType.BikeLane, 32, 76, "3424315", "CA", 92602, "Irvine", DateTime.Now, 0);
            return Ok(this._reportHazardService.Report(hazard));
        }

        [HttpGet]
        [Route("getHazards")]
        public IActionResult GetHazards()
        {
            var hazards = _reportHazardService.GetHazards();
            if (hazards == null)
            {
                return NotFound();
            }
            return Ok(hazards);
        }
        //private readonly IHazardReportingService _hazardReportingService;

        // [Microsoft.AspNetCore.Mvc.Route("excludeHazard")]
        // [Microsoft.AspNetCore.Mvc.HttpPost]
        //public HazardController(ApplicationUser user, Route route) {
        //    this._user = _user;
        //    this._route = route; 
        //    this._hazardExclusionService = new HazardExclusionService(route);
        //}
        // [HttpGet]
        // [Route("exclude")]
        //public bool Exclude(List<HazardType> hazards) {
        //    _hazardExclusionService.FindHazardsNearRoute(hazards);
        //}




    }
}