using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FMS._23.V1.DAL;
using FMS._23.V1.Models;
using FMS._23.V1.REPOSITORY;
using Microsoft.AspNetCore.Authorization;

namespace FMS._23.V1.Controllers
{
	[Authorize]
	public class ItemCategoriesController : Controller
    {
     
		private readonly IRepository<ItemCategories> _Repo;

		public ItemCategoriesController( IRepository<ItemCategories> repository)
        {
          
			this._Repo = repository;
		}
		

		// GET: ItemCategories
		public async Task<IActionResult> Index()
        {
              return _Repo.GetAll() != null ? 
                          View(await _Repo.GetAll().ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ItemCategory'  is null.");
        }
		[Authorize(Roles = "Admin")]
		// GET: ItemCategories/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _Repo.GetAll() == null)
            {
                return NotFound();
            }
            int Id = (int)id;
            var itemCategory = await _Repo.GetById(Id);
            if (itemCategory == null)
            {
                return NotFound();
            }

            return View(itemCategory);
        }
		[Authorize(Roles = "Admin")]
		// GET: ItemCategories/Create
		public IActionResult Create()
        {
            return View();
        }

		// POST: ItemCategories/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "Admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,Active,IsDeleted")] ItemCategories itemCategory)
        {
            if (ModelState.IsValid)
            {
               await _Repo.Add(itemCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(itemCategory);
        }

		// GET: ItemCategories/Edit/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _Repo.GetAll() == null)
            {
                return NotFound();
            }

            var itemCategory = await _Repo.GetById((int)id);
            if (itemCategory == null)
            {
                return NotFound();
            }
            return View(itemCategory);
        }

		// POST: ItemCategories/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "Admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,Active,IsDeleted")] ItemCategories itemCategory)
        {
            if (id != itemCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					await _Repo.Update(itemCategory,id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemCategoryExists(itemCategory.Id))
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
            return View(itemCategory);
        }
		[Authorize(Roles = "Admin")]
		// GET: ItemCategories/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _Repo.GetAll() == null)
            {
                return NotFound();
            }

            var itemCategory = await _Repo.GetById((int)id);
            if (itemCategory == null)
            {
                return NotFound();
            }

            return View(itemCategory);
        }

		// POST: ItemCategories/Delete/5
		[Authorize(Roles = "Admin")]
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_Repo.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ItemCategory'  is null.");
            }
            var itemCategory = await _Repo.GetById((int)id);
            if (itemCategory != null)
            {
				await _Repo.Delete(itemCategory.Id);
            }
            
          
            return RedirectToAction(nameof(Index));
        }

        private bool ItemCategoryExists(int id)
        {
          return (_Repo.GetAll()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
