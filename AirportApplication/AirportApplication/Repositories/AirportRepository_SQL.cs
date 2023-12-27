﻿using AirportApplication.Models.Domain;
using Microsoft.Data.Sqlite;

namespace AirportApplication.Repositories
{
    public class AirportRepository_SQL : IAirportRepository
    {
        private readonly string connectionDB = "Data Source=C:\\Users\\Luka Nikolac\\Documents\\Faks\\DS\\lnikol00-hw-03\\AirportApplication\\AirportApplication\\SQL\\database.db";
        public void CreateNewPlane(Airport plane)
        {
            using var connection = new SqliteConnection(connectionDB);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO Planes (Model, Year, Country, Capacity)
                VALUES ($model, $year, $country, $capacity)";


            command.Parameters.AddWithValue("$model", plane.Model);
            command.Parameters.AddWithValue("$year", plane.Year);
            command.Parameters.AddWithValue("$country", plane.Country);
            command.Parameters.AddWithValue("$capacity", plane.Capacity);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected < 1)
            {
                throw new ArgumentException("Could not insert new plane into database.");
            }
        }

        public void DeletePlane(int id)
        {
            using var connection = new SqliteConnection(connectionDB);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                DELETE FROM Planes
                WHERE ID == $id";

            command.Parameters.AddWithValue("$id", id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected < 1)
            {
                throw new ArgumentException($"No planes with ID = {id}.");
            }
        }

        public List<Airport> GetAllPlanes()
        {
            using var connection = new SqliteConnection(connectionDB);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
             @"    
                   SELECT Planes.ID, Planes.Model, Planes.Year, Planes.Country, Planes.Capacity, PlaneRoutesMap.Routes, PlaneCrewMap.Crew
                   FROM Planes
                   LEFT JOIN PlaneRoutesMap ON Planes.ID = PlaneRoutesMap.PlaneID
                   LEFT JOIN PlaneCrewMap ON Planes.ID = PlaneCrewMap.CrewID"; 

            using var reader = command.ExecuteReader();

            var results = new List<Airport>();
            while (reader.Read())
            {

                var row = new Airport
                {
                    Id = reader.GetInt32(0),
                    Model = reader.GetString(1),
                    Year = reader.GetInt32(2),
                    Country = reader.GetString(3),
                    Capacity = reader.GetInt32(4),
                    Routes = reader.IsDBNull(5) ? new List<string>() : reader.GetString(5).Split(',').ToList(),
                    Crew = reader.IsDBNull(6) ? new List<string>() : reader.GetString(6).Split(',').ToList()
                };

                results.Add(row);
            }

            return results;
        }

        public Airport? GetSinglePlane(int id)
        {
            using var connection = new SqliteConnection(connectionDB);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"     
                   SELECT Planes.ID, Planes.Model, Planes.Year, Planes.Country, Planes.Capacity, PlaneRoutesMap.Routes, PlaneCrewMap.Crew
                   FROM Planes
                   LEFT JOIN PlaneRoutesMap ON Planes.ID = PlaneRoutesMap.PlaneID
                   LEFT JOIN PlaneCrewMap ON Planes.ID = PlaneCrewMap.CrewID 
                   WHERE PLanes.ID == $id";

            command.Parameters.AddWithValue("$id", id);

            using var reader = command.ExecuteReader();

            Airport result = null;

            if (reader.Read())
            {
                result = new Airport
                {
                    Id = reader.GetInt32(0),
                    Model = reader.GetString(1),
                    Year = reader.GetInt32(2),
                    Country = reader.GetString(3),
                    Capacity = reader.GetInt32(4),
                    Routes = reader.IsDBNull(5) ? new List<string>() : reader.GetString(5).Split(',').ToList(),
                    Crew = reader.IsDBNull(6) ? new List<string>() : reader.GetString(6).Split(',').ToList()
                };
            }

            return result;
        }

        public void UpdatePlane(int id, Airport updatedPlane)
        {
            throw new NotImplementedException();
        }
    }
}
