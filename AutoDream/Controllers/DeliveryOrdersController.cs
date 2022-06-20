using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoDream.Data;
using AutoDream.Models;

namespace AutoDream
{
    public class DeliveryOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveryOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeliveryOrders.ToListAsync());
        }

        // GET: DeliveryOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryOrder = await _context.DeliveryOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryOrder == null)
            {
                return NotFound();
            }

            return View(deliveryOrder);
        }

        // GET: DeliveryOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeliveryOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,EngineVolume,EngineType,MaxPrice,OtherInfo,ClientId")] DeliveryOrder deliveryOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryOrder);
                await _context.SaveChangesAsync();
                return Redirect($"/Clients/Delivery?CarId={deliveryOrder.Id}");
            }
            return View(deliveryOrder);
        }

        // GET: DeliveryOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryOrder = await _context.DeliveryOrders.FindAsync(id);
            if (deliveryOrder == null)
            {
                return NotFound();
            }
            return View(deliveryOrder);
        }

        // POST: DeliveryOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,EngineVolume,EngineType,MaxPrice,OtherInfo,ClientId")] DeliveryOrder deliveryOrder)
        {
            if (id != deliveryOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryOrderExists(deliveryOrder.Id))
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
            return View(deliveryOrder);
        }

        // GET: DeliveryOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryOrder = await _context.DeliveryOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryOrder == null)
            {
                return NotFound();
            }

            return View(deliveryOrder);
        }

        // POST: DeliveryOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryOrder = await _context.DeliveryOrders.FindAsync(id);
            _context.DeliveryOrders.Remove(deliveryOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryOrderExists(int id)
        {
            return _context.DeliveryOrders.Any(e => e.Id == id);
        }
    }
}
