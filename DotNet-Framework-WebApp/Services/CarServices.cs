using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DotNet_Framework_WebApp.Models;
using DotNet_Framework_WebApp.ViewModels;

namespace DotNet_Framework_WebApp.Services
{
    public class CarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }
        
        // Get All Cars including Tires
        public List<Car> GetAllCars(bool includeTires = false)
        {
            if (includeTires)
            {
                return _context.Cars.Include(c => c.Tires).ToList();
            }

            return _context.Cars.ToList();
        }

        // Get All Cars including Tires (Store Procedure)
        public List<CarWithTireCount> GetAllCarsWithTireCount()
        {
            using (var context = new AppDbContext())
            {
                return context.Database.SqlQuery<CarWithTireCount>("GetAllCars").ToList();
            }
        }

        // Get a Car by Id including Tires
        public Car GetCarById(int id, bool includeTires = false)
        {
            if (includeTires)
            {
                return _context.Cars.Include(c => c.Tires).SingleOrDefault(car => car.Id == id);
            }

            return _context.Cars.SingleOrDefault(car => car.Id == id);
        }

        // Add a new Car
        public void AddCar(Car car)
        {
            car.CreatedDate = DateTime.Now;
            car.UpdatedDate = DateTime.Now;

            foreach (var tire in car.Tires)
            {
                tire.CreatedDate = DateTime.Now;
                tire.UpdatedDate = DateTime.Now;
            }

            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        // Add Tires to a Car
        public void AddTiresToCar(int carId, List<Tire> tires)
        {
            var car = GetCarById(carId);
            if (car == null) throw new Exception("Car not found");

            foreach (var tire in tires)
            {
                tire.CreatedDate = DateTime.Now;
                car.Tires.Add(tire);
            }

            _context.SaveChanges();
        }

        // Update an existing Car
        public void UpdateCar(Car car)
        {
            var existingCar = GetCarById(car.Id);
            if (existingCar == null) throw new Exception("Car not found");

            existingCar.Brand = car.Brand;
            existingCar.Color = car.Color;
            existingCar.UpdatedDate = DateTime.Now;

            _context.SaveChanges();
        }

        // Delete a Car and its Tires
        public void DeleteCar(int id)
        {
            var car = GetCarById(id, includeTires: true);
            if (car == null) throw new Exception("Car not found");

            // Remove Tires first to satisfy FK constraint if necessary
            if (car.Tires.Any())
            {
                _context.Tires.RemoveRange(car.Tires);
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();
        }
    }
}
