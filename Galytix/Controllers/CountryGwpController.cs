using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Galytix.WebApi.Models;   // <-- Must match your Models namespace
using Galytix.WebApi.Services;// Match your project's namespace

namespace  Galytix.WebApi.Controllers
{
    [ApiController]
    [Route("api/gwp")]
    public class CountryGwpController : ControllerBase
    {
        private readonly GwpDataService _dataService;

        // GwpDataService is injected via constructor
        public CountryGwpController(GwpDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost("avg")]
        public async Task<IActionResult> GetAverageGwp([FromBody] CountryGwpRequest request)
        {
            // Validate the request
            if (request == null || string.IsNullOrEmpty(request.Country) || request.Lob == null)
            {
                return BadRequest("Invalid input data.");
            }

            try
            {
                // Call the service to compute averages
                var result = await _dataService.ComputeAveragesAsync(request.Country, request.Lob);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
