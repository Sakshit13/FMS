using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FMS._23.V1.DAL;
using FMS._23.V1.Models;
using FMS._23.V1.ViewModels;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace FMS._23.V1.Controllers
{
    public class StockTransferTransactionDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public StockTransferTransactionDetailsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
       
        // GET: StockTransferTransactionDetails
        public async Task<IActionResult> Index()
        {
              return _context.StockTransferTransactionDetails != null ? 
                          View(await _context.StockTransferTransactionDetails.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.StockTransferTransactionDetails'  is null.");
        }

        public async Task<IActionResult> STRequests()
        {
            var connectionString = _configuration.GetConnectionString("Conn");
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var query = "sp_GetSTRequest";
                var parameters = new { DepotId = 1 };
                var result = await connection.QueryAsync<STRequestModel>(query,
                                                                         parameters,
                                                                         commandType: CommandType.StoredProcedure);
                return View(result);
            }
        }

        public async Task<IActionResult> STRequestDetals(string RequisitionCode)
        {
            var connectionString = _configuration.GetConnectionString("Conn");
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var query = "STRequestDetailsByRequesitionCode";
                var parameters = new { @RequisitionCode = RequisitionCode };
                var result = await connection.QueryAsync<STRequestDetailsModel>(query,
                                                                         parameters,
                                                                         commandType: CommandType.StoredProcedure);
                return View(result);
            }
        }



        // GET: StockTransferTransactionDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockTransferTransactionDetails == null)
            {
                return NotFound();
            }

            var stockTransferTransactionDetails = await _context.StockTransferTransactionDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockTransferTransactionDetails == null)
            {
                return NotFound();
            }

            return View(stockTransferTransactionDetails);
        }

        // GET: StockTransferTransactionDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockTransferTransactionDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StockTransferByDepot,StockReceivedByDepot,RequisitionCode,StockTransferNo,StockIssueDate,PartNumber,Rate,RequestedQty,IssueQty,BalanceQty,CurrentStock,cGst,sGst,iGst,Discount,IssuedBy,IsActive")] StockTransferTransactionDetails stockTransferTransactionDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockTransferTransactionDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockTransferTransactionDetails);
        }

        // GET: StockTransferTransactionDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockTransferTransactionDetails == null)
            {
                return NotFound();
            }

            var stockTransferTransactionDetails = await _context.StockTransferTransactionDetails.FindAsync(id);
            if (stockTransferTransactionDetails == null)
            {
                return NotFound();
            }
            return View(stockTransferTransactionDetails);
        }

        // POST: StockTransferTransactionDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StockTransferByDepot,StockReceivedByDepot,RequisitionCode,StockTransferNo,StockIssueDate,PartNumber,Rate,RequestedQty,IssueQty,BalanceQty,CurrentStock,cGst,sGst,iGst,Discount,IssuedBy,IsActive")] StockTransferTransactionDetails stockTransferTransactionDetails)
        {
            if (id != stockTransferTransactionDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockTransferTransactionDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockTransferTransactionDetailsExists(stockTransferTransactionDetails.Id))
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
            return View(stockTransferTransactionDetails);
        }

        // GET: StockTransferTransactionDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockTransferTransactionDetails == null)
            {
                return NotFound();
            }

            var stockTransferTransactionDetails = await _context.StockTransferTransactionDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockTransferTransactionDetails == null)
            {
                return NotFound();
            }

            return View(stockTransferTransactionDetails);
        }

        // POST: StockTransferTransactionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockTransferTransactionDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StockTransferTransactionDetails'  is null.");
            }
            var stockTransferTransactionDetails = await _context.StockTransferTransactionDetails.FindAsync(id);
            if (stockTransferTransactionDetails != null)
            {
                _context.StockTransferTransactionDetails.Remove(stockTransferTransactionDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockTransferTransactionDetailsExists(int id)
        {
          return (_context.StockTransferTransactionDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
