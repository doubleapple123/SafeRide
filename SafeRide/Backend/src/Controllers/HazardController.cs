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
    /// <summary>
    /// This class represents the controller for hazard functionality. Is responsible for transferring data between frontend and backend.
    /// </summary>
    [Route("api/hazards")]
    [ApiController]
    [Produces("application/json")]
    
    public class HazardController : ControllerBase
    {
        private readonly IReportHazardService _reportHazardService;

        /// <summary>
        /// Ctor for HazardController. Depends on the ReportHazard service.
        /// </summary>
        /// <param name="reportHazardService">The instance of the ReportHazard service to inject</param>
        public HazardController(IReportHazardService reportHazardService)
        {
            _reportHazardService = reportHazardService;
        }

        /// <summary>
        /// Reports a hazard to the backend.
        /// </summary>
        /// <param name="h">Hazard instance to report</param>
        /// <returns>OkResult upon successful report with JSON reprsenting number of rows affected in underlying database</returns>
        [HttpPost]
        [DisableCors]
        [Route("report")]
        public IActionResult Report(Hazard h)
        {
            Hazard hazard = new Hazard(HazardType.BikeLane, 32, 76, "3424315", "CA", 92602, "Irvine", DateTime.Now, 0);
            return Ok(this._reportHazardService.Report(hazard));
        }

        /// <summary>
        /// Gets available hazards from backend.
        /// </summary>
        /// <returns>OkResult with JSON representing all returned hazards</returns>
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
    }
}