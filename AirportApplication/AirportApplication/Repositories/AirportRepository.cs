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
    public class AirportRepository : IAirportRepository
    {
        // List of all planes
        private List<Airport> m_lstPlanes;

        public AirportRepository()
        {
            // Creating new list
            m_lstPlanes = new List<Airport>();
        }

        // CREATE : Create new plane
        public void CreateNewPlane(Airport plane)
        {
            // Adding new plane to the list
            m_lstPlanes.Add(plane);
        }

        // READ : Get all planes
        public List<Airport> GetAllPlanes()
        {
            // Returns entire list 
            return m_lstPlanes;
        }

        // READ : Get single plane (specified by ID)
        public Airport? GetSinglePlane(int id)
        {
            return m_lstPlanes.FirstOrDefault(e => e.Id == id);
        }

        // DELETE : Delete plane (specified by ID)
        public void DeletePlane(int id)
        {
            Airport? planeToRemove = GetSinglePlane(id);
            if (planeToRemove != null)
            {
                m_lstPlanes.Remove(planeToRemove);
            }
            else
            {
                throw new KeyNotFoundException($"Plane with ID '{id}' not found.");
            }
        }

        public void UpdatePlane(int id, Airport updatedPlane)
        {

            Airport? existingPlane = GetSinglePlane(id);
            if (existingPlane is not null)
            {
                // Update only if the user has permission
                // Implement access control logic as needed
                existingPlane.Model = updatedPlane.Model;
                existingPlane.Year = updatedPlane.Year;
                existingPlane.Country = updatedPlane.Country;
                existingPlane.Capacity = updatedPlane.Capacity;
                existingPlane.Routes = updatedPlane.Routes;
                existingPlane.Crew = updatedPlane.Crew;
            }
            else
            {
                throw new KeyNotFoundException($"Plane with ID '{id}' not found.");
            }
        }
    }
}
