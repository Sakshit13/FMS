using FMS._23.V1.Models;
using FMS._23.V1.REPOSITORY;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FMS._23.V1.Controllers
{
	[Authorize]
	public class VendorController : Controller
    {
        private readonly IRepository<ItemCategories> _Repo;

        public VendorController(IRepository<ItemCategories> repository)
        {

            this._Repo = repository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult VendorDetails()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VendorDetails(VendorDetails vendorDetails)
        {
            if (ModelState.IsValid)
            {
               // await _Repo.Add(vendorDetails);
                return RedirectToAction(nameof(Index));
            }
            else
                return View();
        }

    }
}
