
using FMS._23.V1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FMS._23.V1.Controllers
{
//	[Authorize(Roles = "Admin")]
	public class RolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var role = new IdentityRole { Name = model.RoleName };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserAssignRole()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleList = roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            var user = await _userManager.Users.ToListAsync();
            var userList = user.Select(r => new SelectListItem { Value = r.Id, Text = r.UserName }).ToList();
            var model = new AdminViewModel
            {
                RoleList = roleList,
                UserList = userList
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserAssignRole(AdminViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserID);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }

    }
}
