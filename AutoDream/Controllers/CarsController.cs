using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoDream.Data;
using AutoDream.Models;
using AutoDream.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AutoDream
{
    [Authorize]
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
           //var models = await _context.Models.ToListAsync();
           //var cars = await _context.Cars.ToListAsync();
           var result = _context.Cars.Join(_context.Models, m=>m.ModelId, x=>x.Id, (m,x)=>new
           CarInfo(){
              Id= m.Id,
              Image = m.Image,
              Color = m.Color,
              EngineType = m.EngineType,
              EngineVolume = m.EngineVolume,
              ReleaseYear = m.ReleaseYear,
              ModelName = x.ModelName,
              Type = x.Type
           });
           
           return View(result);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            var model = await _context.Models.FirstOrDefaultAsync(m => m.Id == car.ModelId);
            var price = await _context.CarsPrice.FirstOrDefaultAsync(x => x.Id == car.CarPriceId);
            ViewBag.Price  = price;
            ViewBag.Model = model;
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create(int ModelId)
        {
            ViewBag.Id = ModelId;
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EngineVolume,EngineType,Color,ReleaseYear,VINCode,ModelId,CarPriceId")] CarViewModel carViewModel, IFormFile Image, int ModelId)
        {
            Car car = new Car()
            {
                EngineType = carViewModel.EngineType,
                EngineVolume = carViewModel.EngineVolume,
                Color = carViewModel.Color,
                ReleaseYear = carViewModel.ReleaseYear,
                VINCode = carViewModel.VINCode,
                ModelId = ModelId
            };
            if (ModelState.IsValid)
            {
                
                if (Image != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)Image.Length);
                    }
                    // установка массива байтов
                    car.Image = imageData;
                } 
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectPermanent($"/CarPrice/Create?id={car.Id}");
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EngineVolume,EngineType,Color,ReleaseYear,VINCode,ModelId,CarPriceId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var car = await _context.Cars.FindAsync(id);
            var model = await _context.Models.FindAsync(car.ModelId);
            var price = await _context.CarsPrice.FindAsync(car.CarPriceId);

            _context.Cars.Remove(car);
            _context.Models.Remove(model);
            _context.CarsPrice.Remove(price);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
