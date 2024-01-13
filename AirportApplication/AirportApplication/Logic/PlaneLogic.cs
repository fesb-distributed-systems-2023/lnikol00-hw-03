using AirportApplication.Exceptions;
using AirportApplication.Models;
using AirportApplication.Repositories;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AirportApplication.Logic
{
    public class PlaneLogic : IPlaneLogic
    {
        private readonly IAirportRepository _airportRepository;
        private int _modelMaxCharacters = 20;
        private string _modelRegex = @"^[A-Z][a-zA-Z0-9\s-]*$";
        private string _yearRegex = @"^[1-2]\d{0,3}$";
        private string _countryRegex = @"^[A-Z][a-zA-Z\s]*$";
        private int _countryMaxCharacters = 25;
        private string _capacityRegex = @"^\d+(\.\d+)?$";
        private string _typeRegex = @"^[A-Z][a-zA-Z]*$";
        private string _captainRegex = @"^[A-Z][a-zA-Z\s]*$";
        private int? _captainMaxCharacters = 15;


        public PlaneLogic(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        private void ValidateModel(string? model)
        {
            if (model == null)
            {
                throw new UserErrorMessage("Field can't be empty!");
            }

            if (model.Length > _modelMaxCharacters)
            {
                throw new UserErrorMessage("Exceeded maximum number of characters!");
            }

            if (!Regex.IsMatch(model, _modelRegex))
            {
                throw new UserErrorMessage("Invalid model format! Format must include only letters!");
            }
        }

        private void ValidateYear(int year)
        {

            if (year.ToString() is null)
            {
                throw new UserErrorMessage("Field can't be empty!");
            }

            if (year < 0)
            {
                throw new UserErrorMessage("Year can't be less then 0!");
            }

            if (year.ToString().Length != 4)
            {
                throw new UserErrorMessage("Year must be a 4-digit number!");
            }

            if (!Regex.IsMatch(year.ToString(), _yearRegex))
            {
                throw new UserErrorMessage("Invalid year format! Format must include only positive numbers!");
            }
        }

        private void ValidateCountry(string? country)
        {
            if (country == null)
            {
                throw new UserErrorMessage("Field can't be empty!");
            }

            if (country.Length > _countryMaxCharacters)
            {
                throw new UserErrorMessage("Exceeded maximum number of characters!");
            }

            if (!Regex.IsMatch(country, _countryRegex))
            {
                throw new UserErrorMessage("Invalid country format! Format must include only letters!");
            }
        }

        private void ValidateCapacity(int capacity)
        {
            if (capacity.ToString() is null)
            {
                throw new UserErrorMessage("Field can't be empty!");
            }

            if (capacity < 0 || capacity > 500)
            {
                throw new UserErrorMessage("Capacity can't be less then 0 or more then 500!");
            }

            if (!Regex.IsMatch(capacity.ToString(), _capacityRegex))
            {
                throw new UserErrorMessage("Invalid capacity format! Format must include only positive numbers!");
            }
        }

        private void ValidateType(string? type)
        {
            if (type == null)
            {
                throw new UserErrorMessage("Field can't be empty!");
            }

            if (!Regex.IsMatch(type, _typeRegex))
            {
                throw new UserErrorMessage("Invalid type format!");
            }
        }

        private void ValidateCaptain(string? captain)
        {
            if (captain == null)
            {
                throw new UserErrorMessage("Field can't be empty!");
            }

            if (captain.Length > _captainMaxCharacters)
            {
                throw new UserErrorMessage("Exceeded maximum number of characters!");
            }

            if (!Regex.IsMatch(captain, _captainRegex))
            {
                throw new UserErrorMessage("Invalid captain format. Format must include only letters!");
            }
        }

        public void CreateNewPlane(Airport? plane)
        {
            if (plane is null)
            {
                throw new UserErrorMessage("Cannot create a new plane. All fields must be entered correctly!");
            }

            plane.Id = -1;
            ValidateModel(plane.Model);
            ValidateYear(plane.Year);
            ValidateCountry(plane.Country);
            ValidateCapacity(plane.Capacity);
            ValidateType(plane.Type);
            ValidateCaptain(plane.Captain);

            _airportRepository.CreateNewPlane(plane);
        }

        public void UpdatePlane(int id, Airport? plane)
        {
            if (plane is null)
            {
                throw new UserErrorMessage("Cannot update plane. All fields must be entered correctly!");
            }

            plane.Id = -1;
            ValidateModel(plane.Model);
            ValidateYear(plane.Year);
            ValidateCountry(plane.Country);
            ValidateCapacity(plane.Capacity);
            ValidateType(plane.Type);
            ValidateCaptain(plane.Captain);

            _airportRepository.UpdatePlane(id, plane);
        }

        public void DeletePlane(int id)
        {
            _airportRepository.DeletePlane(id);
        }

        public Airport? GetSinglePlane(int id)
        {
            return _airportRepository.GetSinglePlane(id);
        }

        public IEnumerable<Airport> GetAllPlanes()
        {
            return _airportRepository.GetAllPlanes();
        }
    }
}
