using System;
using CarAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarAPI.Database
{
    public class DatabaseSeeder
    {
        private readonly CarContext _carContext;

        public DatabaseSeeder(CarContext carContext)
        {
            this._carContext = carContext;
        }

        public void Seed()
        {
            Console.WriteLine("Seeding...");
            AddCars();
            AddTelemetries();
        }

        private void AddCars()
        {
            if (!_carContext.Cars.Any())
            {
                var cars = new List<Car>()
                {
                        new Car()
                        {
                            IdCar = 1,
                            Name = "Bestes Auto",
                            Type = "Mercedes"
                        },
                        new Car()
                        {
                            IdCar = 2,
                            Name = "2. Bestes Auto",
                            Type = "Audi"
                        },
                        new Car()
                        {
                            IdCar = 3,
                            Name = "3. Bestes Auto",
                            Type = "BMW"
                        }
                };

                _carContext.Cars.AddRange(cars);
                _carContext.SaveChanges();
            };
        }

        private void AddTelemetries()
        {
            if (!_carContext.Cars.Any())
            {
                var cars = new List<Car>()
                {
                        new Car()
                        {
                            IdCar = 1,
                            Name = "Bestes Auto",
                            Type = "Mercedes"
                        },
                        new Car()
                        {
                            IdCar = 2,
                            Name = "2. Bestes Auto",
                            Type = "Audi"
                        },
                        new Car()
                        {
                            IdCar = 3,
                            Name = "3. Bestes Auto",
                            Type = "BMW"
                        }
                };

                _carContext.Cars.AddRange(cars);
                _carContext.SaveChanges();
            };
        }
    }
}

