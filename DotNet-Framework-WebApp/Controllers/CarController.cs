using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DotNet_Framework_WebApp.Models;
using DotNet_Framework_WebApp.Services;

namespace DotNet_Framework_WebApp.Controllers
{
    public class CarController : Controller
    {
        private readonly CarService _carService;

        public CarController()
        {
            var context = new AppDbContext();
            _carService = new CarService(context);
        }

        // GET: Car
        public ActionResult Index()
        {
            var cars = _carService.GetAllCars(includeTires: true);
            return View(cars);
        }

        // GET: Car/Details/{id}
        public ActionResult Details(int id)
        {
            var car = _carService.GetCarById(id, includeTires: true);
            if (car == null)
            {
                return HttpNotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                _carService.AddCar(car); // Ini juga akan menyimpan Tire karena relasi
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Car/Edit/{id}
        public ActionResult Edit(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                return HttpNotFound();
            }

            return View(car);
        }

        // POST: Car/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                _carService.UpdateCar(car);
                return RedirectToAction("Index");
            }

            return View(car);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // Panggil service untuk menghapus car
                _carService.DeleteCar(id);
        
                // Redirect ke Index setelah berhasil menghapus
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Tangani jika ada error
                ModelState.AddModelError("", "Terjadi kesalahan saat menghapus data: " + ex.Message);
                return RedirectToAction("Index");
            }
        }


        // POST: Car/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _carService.DeleteCar(id);
            return RedirectToAction("Index");
        }

        // GET: Car/AddTires/{id}
        public ActionResult AddTires(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                return HttpNotFound();
            }

            ViewBag.CarId = car.Id;
            return View();
        }

        // POST: Car/AddTires
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTires(int carId, List<Tire> tires)
        {
            if (tires != null && tires.Count > 0)
            {
                _carService.AddTiresToCar(carId, tires);
                return RedirectToAction("Details", new { id = carId });
            }

            return View();
        }
    }
}
