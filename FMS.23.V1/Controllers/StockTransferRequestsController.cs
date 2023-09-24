using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FMS._23.V1.DAL;
using FMS._23.V1.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace FMS._23.V1.Controllers
{
    public class StockTransferRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public StockTransferRequestsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            this._configuration = configuration;

        }

        // GET: StockTransferRequests
        public async Task<IActionResult> Index()
        {
            try
            {

            
            var applicationDbContext = _context.StockTransferPartRequest.Include(s => s.DepotMaster);
                return View(await applicationDbContext.ToListAsync());

            }
            catch(Exception ex)
            {
                return View(null);
            }

        }

        [HttpPost]
        public ActionResult GetCurrentStock(string partNumber, int depotId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    connection.Open();

                    // Create a command to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("sp_GetCurrentStockByPartNumAndDepot", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@PartNumber", partNumber));
                        command.Parameters.Add(new SqlParameter("@DepotId", depotId));

                        // Execute the command
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Retrieve the StockQuantity from the result
                                decimal stockQuantity = reader.GetDecimal(reader.GetOrdinal("StockQuantity"));

                                var stockInfoHtml = stockQuantity.ToString();
                                return Content(stockInfoHtml, "text/html");
                            }
                        }
                    }
                }

                // If no data was found or an error occurred, handle it appropriately
                return Content("No data found.", "text/html");
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors as needed
                return null;
            }
        }

        // GET: StockTransferRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockTransferPartRequest == null)
            {
                return NotFound();
            }

            var stockTransferPartRequest = await _context.StockTransferPartRequest
                .Include(s => s.DepotMaster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockTransferPartRequest == null)
            {
                return NotFound();
            }

            return View(stockTransferPartRequest);
        }

        // GET: StockTransferRequests/Create
        public IActionResult Create()
        {
            ViewBag.RequestedToDepot = new SelectList(_context.Set<DepotMaster>(), "Id", "DepotName");
            ViewBag.PartNumber = new SelectList(_context.ItemMaster, "PartNumber", "PartNumber");
            return View();
        }

        [HttpPost]
        public IActionResult Create(string jsonData, string RequisitionCode, DateTime RequestedDate, string RequestedToDepot)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Conn"));
                con.Open(); // Open the connection

                SqlCommand cmd = new SqlCommand("sp_InsertStockTransferPartRequest", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters to the command
                cmd.Parameters.Add("@jsonData", SqlDbType.NVarChar, -1).Value = jsonData; // Use -1 for MAX length

                cmd.Parameters.Add("@RequisitionCode", SqlDbType.NVarChar, 40).Value = RequisitionCode;
                cmd.Parameters.Add("@RequestedDate", SqlDbType.DateTime).Value = RequestedDate;
                cmd.Parameters.Add("@RequestedToDepot", SqlDbType.Int).Value =Convert.ToInt32(RequestedToDepot);

                cmd.ExecuteNonQuery(); // Execute the Stored Procedure

                con.Close(); // Close the connection
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, or return an error response as needed
            }

            return View();
        }

        //// POST: StockTransferRequests/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,RequisitionCode,PartNumber,RequestQty,Remarks,RequestedByDepot,RequestedToDepot,RequestedDate,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsActive,IsDeleted")] StockTransferPartRequest stockTransferPartRequest)
        //{
        //    stockTransferPartRequest.IsActive = true;
        //    stockTransferPartRequest.IsDeleted = false;


        //    if (!ModelState.IsValid)
        //    {
        //        _context.Add(stockTransferPartRequest);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["RequestedToDepot"] = new SelectList(_context.Set<DepotMaster>(), "Id", "DepotName", stockTransferPartRequest.RequestedToDepot);
        //    return View(stockTransferPartRequest);
        //}

        // GET: StockTransferRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockTransferPartRequest == null)
            {
                return NotFound();
            }

            var stockTransferPartRequest = await _context.StockTransferPartRequest.FindAsync(id);
            if (stockTransferPartRequest == null)
            {
                return NotFound();
            }
            ViewData["RequestedToDepot"] = new SelectList(_context.Set<DepotMaster>(), "Id", "DepotName", stockTransferPartRequest.RequestedToDepot);
            return View(stockTransferPartRequest);
        }

        // POST: StockTransferRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequisitionCode,PartNumber,RequestQty,Remarks,RequestedByDepot,RequestedToDepot,RequestedDate,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsActive,IsDeleted")] StockTransferPartRequest stockTransferPartRequest)
        {
            if (id != stockTransferPartRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockTransferPartRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockTransferPartRequestExists(stockTransferPartRequest.Id))
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
            ViewData["RequestedToDepot"] = new SelectList(_context.Set<DepotMaster>(), "Id", "DepotName", stockTransferPartRequest.RequestedToDepot);
            return View(stockTransferPartRequest);
        }

        // GET: StockTransferRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockTransferPartRequest == null)
            {
                return NotFound();
            }

            var stockTransferPartRequest = await _context.StockTransferPartRequest
                .Include(s => s.DepotMaster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockTransferPartRequest == null)
            {
                return NotFound();
            }

            return View(stockTransferPartRequest);
        }

        // POST: StockTransferRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockTransferPartRequest == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StockTransferPartRequest'  is null.");
            }
            var stockTransferPartRequest = await _context.StockTransferPartRequest.FindAsync(id);
            if (stockTransferPartRequest != null)
            {
                _context.StockTransferPartRequest.Remove(stockTransferPartRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockTransferPartRequestExists(int id)
        {
          return (_context.StockTransferPartRequest?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
