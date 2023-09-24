using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FMS._23.V1.DAL;
using FMS._23.V1.ViewModels;
using FMS._23.V1.Models;
using FMS._23.V1.REPOSITORY;
using FMS._23.V1.Helpers;
using Microsoft.Data.SqlClient;
using FMS._23.V1.BAL;


namespace FMS._23.V1.Controllers
{
    public class ItemMasterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ItemMasterBAL _itemmasterbal;
        public const string spInsertPartDetails = "spInsertPartDetails";
        public const string sp_UpdatePartDetails = "sp_UpdatePartDetails";
        private readonly IRepository<ItemViewModel> _Repo;

        public ItemMasterController(ApplicationDbContext context, IRepository<ItemViewModel> repository, ItemMasterBAL itemmasterbal)
        {
            _context = context;
            this._Repo = repository;
            _itemmasterbal = itemmasterbal;
        }



        // GET: ItemMaster
        public async Task<IActionResult> Index()
        {
            var a = _itemmasterbal.GetItemList();

            return View(a);
        }

        [HttpGet]
        public IActionResult GetPartNumberSuggestions(string input)
        {
            // Query your database or data source to fetch suggestions based on the input
            // For example, you can use LINQ to filter results
            var suggestions = _context.ItemMaster
                .Where(item => item.PartNumber.Contains(input))
                .Select(item => item.PartNumber)
                .ToList();

            return Json(suggestions);
        }


        // GET: ItemMaster/Create
        public IActionResult Create()
        {
        
            
            //ViewData["DepotId"] = new SelectList(_context.Set<DepotMaster>(), "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "Id", "CategoryName");
            ViewData["UnitOfMeasurmentId"] = new SelectList(_context.UnitOfMeasurment, "Id", "UnitName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ItemViewModel itemViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    SqlParameter[] parameters =
           {
              new SqlParameter("@PartNumber",itemViewModel.PartNumber),

                new SqlParameter("@Rate", itemViewModel.Rate),
                new SqlParameter("@Discount", itemViewModel.Discount),
                new SqlParameter("@Igst", itemViewModel.Igst),
                new SqlParameter("@Cgst", itemViewModel.Cgst),
                new SqlParameter("@Sgst", itemViewModel.Sgst),
                new SqlParameter("@Location", itemViewModel.Location),
                new SqlParameter("@MinimumStock", itemViewModel.MinimumStock),
                new SqlParameter("@MRP", itemViewModel.Mrp),
                new SqlParameter("@Description", itemViewModel.Description),
                new SqlParameter("@AltPartNum", itemViewModel.AltPartNum),
                new SqlParameter("@Remarks", itemViewModel.Remarks),
                new SqlParameter("@CategoryId", itemViewModel.CategoryId),
                new SqlParameter("@UnitOfMeasurmentId", itemViewModel.UnitOfMeasurmentId)
            };

                    int result = _Repo.ExecuteCommand(spInsertPartDetails, parameters);

                    if (result > 0)
                    {
                        // Successful execution, redirect to the desired page
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Handle the case when the stored procedure did not execute successfully
                        ModelState.AddModelError(string.Empty, "Failed to insert data.");
                    }
                }

                ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "Id", "Id", itemViewModel.CategoryId);
                ViewData["UnitOfMeasurmentId"] = new SelectList(_context.UnitOfMeasurment, "Id", "Id", itemViewModel.UnitOfMeasurmentId);
                return View(itemViewModel);
            }
            catch (Exception ex)
            {
                // Handle any unhandled exceptions here
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(itemViewModel);
            }
        }

        // GET: ItemMaster/Edit/5
        public async Task<IActionResult> Edit(string? PartNumber)
        {
            try
            {
                if (PartNumber == null || _itemmasterbal.GetItemList() == null)
                {
                    return NotFound();
                }

                var itemViewModel = _itemmasterbal.GetItemList(PartNumber);
                if (itemViewModel == null)
                {
                    return NotFound();
                }

                ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "Id", "CategoryName", itemViewModel.CategoryId);
                ViewData["UnitOfMeasurmentId"] = new SelectList(_context.UnitOfMeasurment, "Id", "UnitName", itemViewModel.UnitOfMeasurmentId);
                return View(itemViewModel);
            }
            catch (Exception ex) {
                string errormessage = ex.Message;
                return View(errormessage);
            }

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemViewModel itemViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    SqlParameter[] parameters =
           {
              new SqlParameter("@PartNumber",itemViewModel.PartNumber),

                new SqlParameter("@Rate", itemViewModel.Rate),
                new SqlParameter("@Discount", itemViewModel.Discount),
                new SqlParameter("@Igst", itemViewModel.Igst),
                new SqlParameter("@Cgst", itemViewModel.Cgst),
                new SqlParameter("@Sgst", itemViewModel.Sgst),
                new SqlParameter("@Location", itemViewModel.Location),
                new SqlParameter("@MinimumStock", itemViewModel.MinimumStock),
                new SqlParameter("@MRP", itemViewModel.Mrp),
                new SqlParameter("@Description", itemViewModel.Description),
                new SqlParameter("@AltPartNum", itemViewModel.AltPartNum),
                new SqlParameter("@Remarks", itemViewModel.Remarks),
                new SqlParameter("@CategoryId", itemViewModel.CategoryId),
                new SqlParameter("@UnitOfMeasurmentId", itemViewModel.UnitOfMeasurmentId)
            };

                    int result = _Repo.ExecuteCommand(sp_UpdatePartDetails, parameters);

                    if (result > 0)
                    {
                        // Successful execution, redirect to the desired page
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Handle the case when the stored procedure did not execute successfully
                        ModelState.AddModelError(string.Empty, "Failed to insert data.");
                    }
                }

                ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "Id", "Id", itemViewModel.CategoryId);
                ViewData["UnitOfMeasurmentId"] = new SelectList(_context.UnitOfMeasurment, "Id", "Id", itemViewModel.UnitOfMeasurmentId);
                return View(itemViewModel);
            }
            catch (Exception ex)
            {
                // Handle any unhandled exceptions here
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(itemViewModel);
            }
        }

        // GET: ItemMaster/Delete/5
        public async Task<IActionResult> Delete(string? PartNumber)
        {
            ViewBag.message = PartNumber;
            return View();
        }

        [HttpPost]
        public JsonResult CheckDuplicatePartNumber(string partNumber)
        {
            bool isDuplicate = _context.ItemMaster
                .Any(i => i.PartNumber.Trim().ToLower() == partNumber.Trim().ToLower());

            return Json(!isDuplicate);
        }





        // POST: ItemMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? PartNumber)
        {
            if (ModelState.IsValid)
            {
                SqlParameter[] parameters =
            {
              new SqlParameter("@PartNumber",PartNumber),
                new SqlParameter("@Action","Delete")
            };

                int result = _Repo.ExecuteCommand(sp_UpdatePartDetails, parameters);

                if (result > 0)
                {
                    // Successful execution, redirect to the desired page
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the case when the stored procedure did not execute successfully
                    ModelState.AddModelError(string.Empty, "Failed to insert data.");
                }

            }
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            return View();
        }

     


    }
}
