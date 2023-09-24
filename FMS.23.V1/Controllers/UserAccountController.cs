
using FMS._23.V1.Models;
using FMS._23.V1.REPOSITORY;
using FMS._23.V1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FMS._23.V1.Controllers
{
	[Authorize]
	public class UserAccountController : Controller
    {
        private IRepository<VendorDetails> _Repo;

        public UserAccountController(IRepository<VendorDetails> repository)
        {
            this._Repo=repository;
        }
		public async Task<IActionResult> Index()
        {
			var data = await _Repo.GetAllRow(); // Await the asynchronous method.
			return View(data); // Pass the data to your view.
		}

        public IActionResult MasterList()
        {
            var VenList = _Repo.GetAll().Select(r => new SelectListItem { Value = r.Gstnumber, Text = r.VendorName }).ToList();

            var model = new MasterViewModel
            {
                VendorList = VenList
            };
            return View(model);

        }

	}
}
