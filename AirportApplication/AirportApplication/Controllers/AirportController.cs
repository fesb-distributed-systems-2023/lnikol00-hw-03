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

using AirportApplication.Controllers.DTO;
using AirportApplication.Models;
using AirportApplication.Filters;
using AirportApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirportApplication.Controllers
{
    [LogFilter]
    [ApiController]
    [Route("api/[controller]")]
    public class AirportController : ControllerBase
    {
        private readonly IAirportRepository _airportRepository;

        public AirportController(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        [HttpPost]
        public ActionResult CreateNewPlane([FromBody] NewPlaneDTO plane) 
        {
            if (plane == null)
            {
                return BadRequest($"Incorect format!");

            }
           _airportRepository.CreateNewPlane(plane.ToModel());

            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlaneInfoDTO>> GetAllPlanes()
        {
            var allPlanes = _airportRepository.GetAllPlanes().Select(x => PlaneInfoDTO.FromModel(x));
            return Ok(allPlanes);
        }

        [HttpGet("{id}")]
        public ActionResult<PlaneInfoDTO> GetSinglePlane(int id)
        {
            var plane = _airportRepository.GetSinglePlane(id);

            if(plane is null)
            {
                return NotFound($"Plane with id:{id} doesn't exist!");
            }
            else
            {
                return Ok(PlaneInfoDTO.FromModel(plane));
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePlane(int id)
        {
            var plane = _airportRepository.GetSinglePlane(id);
            if(plane is null)
            {
                return NotFound($"Plane with id:{id} doesn't exist!");
            }

            _airportRepository.DeletePlane(id);

            return Ok();
        }

        [HttpPut("/planes/{id}")]
        public ActionResult UpdatePlane(int id, [FromBody] NewPlaneDTO updatedPlane)
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

            _airportRepository.UpdatePlane(id, updatedPlane.ToModel());

            return Ok();
        }
    }
}
