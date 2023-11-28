/*
**********************************
* Author: Luka Nikolac
* Project Task: Airport - Phase 2
**********************************
* Description:
*  
*  Implement `IAirportRepository` interface
*
**********************************
*/

using Microsoft.AspNetCore.Mvc;
using AirportApplication.Models.Domain;
using AirportApplication.Repositories;

namespace AirportApplication.Repositories
{
    public class AirportRepository
    {
        // List of all planes
        private List<Airport> m_lstPlanes;

        public AirportRepository()
        {
            // Creating new list
            m_lstPlanes = new List<Airport>();
        }

        // CREATE : Create new plane
        public bool CreateNewPlane(Airport plane)
        {
            // Adding new plane to the list
            m_lstPlanes.Add(plane);

            return true;
        }

        // READ : Get all planes
        public IEnumerable<Airport> GetAllPlanes()
        {
            // Returns entire list 
            return m_lstPlanes;
        }

        // READ : Get single plane (specified by ID)
        public Airport GetSinglePlane(int id)
        {
            if (!m_lstPlanes.Any(plane => plane.Id == id))
            {
                // Checks if any plane matches currently used id, if not returns null
                return null;
            }

            var plane = m_lstPlanes.FirstOrDefault(plane => plane.Id == id);    

            // Checks if plane matches an id, if yes returns that plane
            return plane;
        }

        // DELETE : Delete plane (specified by ID)
        public bool DeletePlane(int id)
        {
            // Check if plane matches ID
            var planeToDelete = m_lstPlanes.FirstOrDefault(itemPlane => itemPlane.Id == id);
            if(planeToDelete == null)
            {
                return false;
            }

            m_lstPlanes.Remove(planeToDelete);

            return true;
        }
    }
}
