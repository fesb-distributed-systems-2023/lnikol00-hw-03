/*
**********************************
* Author: Luka Nikolac
* Project Task: Airport - Phase 1
**********************************
* Description:
*  
*  Contains Model defining Airport class 
*
**********************************
*/



namespace AirportApplication.Models.Domain
{
    public class Airport
    {
        // Unique ID for each plane
        public int Id { get; set; }

        // Plane model (name)
        public string? Model { get; set; }

        // Year plane was built
        public string? Year { get; set; }

        // Country origin (Country where plane was built)
        public string? Country { get; set; }

        // Number of passengers available on flight
        public int Capacity { get; set; }

        // List of cities to which the plane travels 
        // Example: Dubrovnik-Zagreb, Zagreb-London ...
        public List<string>? Routes { get; set; }

        // List of crew members currently aboard flight
        public List<string>? Crew { get; set; }
    }
}
