/*
**********************************
* Author: Luka Nikolac
* Project Task: Airport - Phase 2
**********************************
* Description:
* 
*    CREATE - Add new plane
*    READ - Get all planes
*    READ - Get specific plane
*    DELETE - Delete plane
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
        private readonly IAirportRepository _airportRepository;

        public AirportController(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        // CREATE
        // Postman request: POST http://localhost:5079/planes/new
        // Postman response: New plane created!
        /* Postman body:
            * {
                 "id": 1,
                 "model":"Plane 1",
                 "year":"2000",
                 "country": "Croatia",
                 "capacity": 2000,
                 "routes":["Miami", "New York", "Washington DC"],
                 "crew":["Andrew", "Floyd", "Stephanie"]
               }           
         */
        [HttpPost("/planes/new")]
        public IActionResult CreateNewPlane([FromBody] Airport plane) 
        {
           _airportRepository.CreateNewPlane(plane);

            return Ok();
        }

        // GET ALL
        // Postman request: GET http://localhost:5079/planes/all
        /* Postman response : 
         * [
                {
                   "id": 1,
                   "model": "Plane 1",
                   "year": "2000",
                   "country": "Croatia",
                   "capacity": 2000,
                   "routes": [
                        "Miami",
                        "New York",
                        "Washington DC"
                   ],
                   "crew": [
                       "Andrew",
                       "Floyd",
                       "Stephanie"
                    ]
                },
                {
                    "id": 2,
                    "model": "Plane 2",
                    "year": "2001",
                    "country": "America",
                    "capacity": 1800,
                    "routes": [
                        "Bla",
                        "Bla",
                        "Blab DC"
                    ],
                    "crew": [
                        "One",
                        "Two",
                        "Three"
                    ]
                }
        
           ]
        */
        [HttpGet("/planes/all")]
        public IActionResult GetAllPlanes()
        {
            return Ok(_airportRepository.GetAllPlanes());
        }

        // GET SINGLE PLANE
        // Postman request (success): GET http://localhost:5079/planes/2
        // Postman response (success): 
        /* 
           {
                "id": 2,
                "model": "Plane 2",
                "year": "2001",
                "country": "America",
                "capacity": 1800,
                "routes": [
                    "Bla",
                    "Bla",
                    "Blab DC"
                ],
                "crew": [
                    "One",
                    "Two",
                    "Three"
                ]
            }
         */
        // Postman request (error): GET http://localhost:5079/planes/12
        // Postman response (error): Plane with id:12 doesn't exist!
        [HttpGet("/planes/{id}")]
        public IActionResult GetSinglePlane([FromRoute]int id)
        {
            var plane = _airportRepository.GetSinglePlane(id);

            if(plane is null)
            {
                return NotFound($"Plane with id:{id} doesn't exist!");
            }
            else
            {
                return Ok(plane);
            }
        }

        // DELETE
        // Postman request (success): DELETE http://localhost:5079/planes/2
        // Postman response (success): Deleted plane with id=2!
        // Postman request (error): DELETE http://localhost:5079/planes/12
        // Postman response (error): Could not find plane with id=12!
        [HttpDelete("/planes/{id}")]
        public IActionResult DeletePlane([FromRoute]int id)
        {
            if(_airportRepository.DeletePlane(id))
            {
                return Ok($"Deleted plane with id={id}!");
            }
            else
            {
                return NotFound($"Could not find plane with id={id}!");
            }
        }

        [HttpPut("/planes/{id}")]
        public ActionResult UpdatePlane(int id, [FromBody] Airport updatedPlane)
        {
            if (updatedPlane == null)
            {
                return BadRequest();
            }

            var existingPlane = _airportRepository.GetSinglePlane(id);
            if (existingPlane == null)
            {
                return NotFound();
            }

            _airportRepository.UpdatePlane(id, updatedPlane);

            return Ok();
        }
    }
}
