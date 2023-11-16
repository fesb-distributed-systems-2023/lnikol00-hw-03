/*
**********************************
* Author: Luka Nikolac
* Project Task: Airport - Phase 2
**********************************
* Description:
* 
*    Create - Add new plane
*    Read - Get all planes
*    Read - Get specific plane
*    Delete - Delete plane
*
**********************************
*/

using AirportApplication.Models.Domain;
using AirportApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirportApplication.Controllers
{
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportRepository _airportRepository;

        public AirportController(AirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        // CREATE
        // Postman request: POST http://localhost:5079/planes/new
        // Postman response: New plane created!
        [HttpPost("/planes/new")]
        public IActionResult CreateNewEmail([FromBody] Airport plane) 
        {
            bool fSuccess = _airportRepository.CreateNewPlane(plane);

            if(fSuccess)
            {
                return Ok("New plane created!");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        // GET ALL

    }
}
