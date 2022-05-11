using System.Text.RegularExpressions;
using System.Web.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SafeRide.src.Interfaces;
using SafeRide.src.Models;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
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
        [Route("report")]
        public IActionResult Report(double lat, double lon, int type)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                Hazard h = new Hazard((HazardType)type, lat, lon, "user", " ", 00000, " ", TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Pacific Standard Time"), 0);

                return Ok(this._reportHazardService.Report(h));
            }catch (Exception ex)
            {
                return StatusCode(500, "Meow");
            }
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
            if (hazards == null || hazards.Count == 0)
            {
                return NotFound();
            }
            return Ok(hazards);
        }
    }
}