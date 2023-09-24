using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FMS._23.V1.DAL;
using FMS._23.V1.Models;

namespace FMS._23.V1.Controllers
{
    public class UnitOfMeasurementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnitOfMeasurementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UnitOfMeasurement
        public async Task<IActionResult> Index()
        {
              return _context.UnitOfMeasurment != null ? 
                          View(await _context.UnitOfMeasurment.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.UnitOfMeasurment'  is null.");
        }

        // GET: UnitOfMeasurement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UnitOfMeasurment == null)
            {
                return NotFound();
            }

            var unitOfMeasurment = await _context.UnitOfMeasurment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitOfMeasurment == null)
            {
                return NotFound();
            }

            return View(unitOfMeasurment);
        }

        // GET: UnitOfMeasurement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnitOfMeasurement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UnitName,UnitShortName,UnitOf,Active,IsDeleted")] UnitOfMeasurment unitOfMeasurment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unitOfMeasurment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitOfMeasurment);
        }

        // GET: UnitOfMeasurement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UnitOfMeasurment == null)
            {
                return NotFound();
            }

            var unitOfMeasurment = await _context.UnitOfMeasurment.FindAsync(id);
            if (unitOfMeasurment == null)
            {
                return NotFound();
            }
            return View(unitOfMeasurment);
        }

        // POST: UnitOfMeasurement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UnitName,UnitShortName,UnitOf,Active,IsDeleted")] UnitOfMeasurment unitOfMeasurment)
        {
            if (id != unitOfMeasurment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unitOfMeasurment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitOfMeasurmentExists(unitOfMeasurment.Id))
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
            return View(unitOfMeasurment);
        }

        // GET: UnitOfMeasurement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UnitOfMeasurment == null)
            {
                return NotFound();
            }

            var unitOfMeasurment = await _context.UnitOfMeasurment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitOfMeasurment == null)
            {
                return NotFound();
            }

            return View(unitOfMeasurment);
        }

        // POST: UnitOfMeasurement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UnitOfMeasurment == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UnitOfMeasurment'  is null.");
            }
            var unitOfMeasurment = await _context.UnitOfMeasurment.FindAsync(id);
            if (unitOfMeasurment != null)
            {
                _context.UnitOfMeasurment.Remove(unitOfMeasurment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitOfMeasurmentExists(int id)
        {
          return (_context.UnitOfMeasurment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
