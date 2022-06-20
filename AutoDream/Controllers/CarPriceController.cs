using System.Threading.Tasks;
using AutoDream.Data;
using AutoDream.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoDream.Controllers
{
    [Authorize]
    public class CarPriceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarPriceController(ApplicationDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        // GET: CarPrice/Create
        public IActionResult Create(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // POST: CarPrice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarPrice carPrice, int CarId)
        {
            if (ModelState.IsValid)
            {
                await _context.CarsPrice.AddAsync(carPrice);
                await _context.SaveChangesAsync();
                var car = await _context.Cars.FindAsync(CarId);
                car.CarPriceId = carPrice.Id;
                _context.Cars.Update(car);
                await _context.SaveChangesAsync();


                return RedirectPermanent("/Cars");
            }
            return View(carPrice);
        }
    }
}
