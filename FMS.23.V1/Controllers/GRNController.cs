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

namespace FMS._23.V1.Controllers
{
    public class GRNController : Controller
    {
		private readonly IRepository<GrntransactionDetail> _Repo;

		public GRNController(IRepository<GrntransactionDetail> repository)
		{

			this._Repo = repository;
		}

		// GET: GRN
		public async Task<IActionResult> Index()
        {
              return _Repo.GetAll() != null ? 
                          View(await _Repo.GetAll().ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GrntransactionDetail'  is null.");
        }

        // GET: GRN/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _Repo.GetAll() == null)
            {
                return NotFound();
            }
			int Id = (int)id;
			var grntransactionDetail = await _Repo.GetById(Id);
            if (grntransactionDetail == null)
            {
                return NotFound();
            }

            return View(grntransactionDetail);
        }

        // GET: GRN/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Grntid,Grnnumber,PartNumber,RecievedQty,AcceptedQty,RejectedQty,UnitPrice,Tax,Discount,Cgst,Sgst,Igst,Tcs,Cartage,FreightAmount,OpeningStock,ClosingStock,Active,IsDeleted,Remarks,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,UploadInvoice")] GrntransactionDetail grntransactionDetail, IFormFile UploadInvoice)
        {
            if (ModelState.IsValid)
            {
                if (UploadInvoice != null && UploadInvoice.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await UploadInvoice.CopyToAsync(memoryStream);
                        grntransactionDetail.UploadInvoice = memoryStream.ToArray();
                    }
                }

                await _Repo.Add(grntransactionDetail);
                return RedirectToAction(nameof(Index));
            }
            return View(grntransactionDetail);
        }

        // GET: GRN/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _Repo.GetAll() == null)
            {
                return NotFound();
            }

            var grntransactionDetail = await _Repo.GetById((int)id);
            if (grntransactionDetail == null)
            {
                return NotFound();
            }
            ViewData["EditMode"] = true;
            return View(grntransactionDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile UploadInvoice, [Bind("Grntid,Grnnumber,PartNumber,RecievedQty,AcceptedQty,RejectedQty,UnitPrice,Tax,Discount,Cgst,Sgst,Igst,Tcs,Cartage,FreightAmount,OpeningStock,ClosingStock,Active,IsDeleted,Remarks,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,UploadInvoice")] GrntransactionDetail grntransactionDetail)
        {
            if (id != grntransactionDetail.Grntid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (UploadInvoice != null && UploadInvoice.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await UploadInvoice.CopyToAsync(memoryStream);
                            grntransactionDetail.UploadInvoice = memoryStream.ToArray();
                        }
                    }

                    await _Repo.Update(grntransactionDetail,id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrntransactionDetailExists(grntransactionDetail.Grntid))
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
            
           
            return View(grntransactionDetail);
        }

        // GET: GRN/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (_Repo.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GrntransactionDetail'  is null.");
            }
            var grntransactionDetail = await _Repo.GetById((int)id);

            if (grntransactionDetail != null)
            {
                await _Repo.Delete(grntransactionDetail.Grntid);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GrntransactionDetailExists(int id)
        {
          return (_Repo.GetAll()?.Any(e => e.Grntid == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> DownloadInvoice(int id)
        {
            var grntransactionDetail = await _Repo.GetById(id);

            if (grntransactionDetail == null || grntransactionDetail.UploadInvoice == null)
            {
                return NotFound();
            }
            string contentType = "image/jpeg"; 
            string fileName = "invoice.jpg"; 
            return File(grntransactionDetail.UploadInvoice, contentType, fileName);
        }
    }
}
