using Castle.Core.Smtp;
using FMS._23.V1.DAL;
using FMS._23.V1.REPOSITORY;
using FMS._23.V1.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FMS._23.V1.Controllers
{
	//[Authorize(Roles = "Admin")]
	public class AdminAccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		// private readonly IEmailSender _emailSender; // Optional, if you want to send reset emails
		private readonly RoleManager<IdentityRole> _roleManager;

		private readonly ApplicationDbContext _context;

		public AdminAccountController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			//  _emailSender = emailSender;
			_roleManager = roleManager;
			_context = context;
		}
		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			var user = await _userManager.FindByNameAsync(model.Email);
			if (user != null)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

				var userRoles = await _userManager.GetRolesAsync(user);
				var roleId = userRoles.FirstOrDefault();

				// Store roleId as a claim
				var claims = new List<Claim>
					 {
					new Claim(ClaimTypes.Name, user.UserName),
					 new Claim(ClaimTypes.Role, roleId),
                     // Add other claims as needed
                    };

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[AllowAnonymous]
		private IActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
		}
	//	[AllowAnonymous]
		[HttpGet]
		public IActionResult Register()
		{
			var roles = _roleManager.Roles.ToList();
			var roleList = roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
			//var DepotList = _context.DepotMaster.Select(r => new SelectListItem { Value = Convert.ToString(r.Id), Text = r.DepotName }).ToList();
			var model = new AdminViewModel
			{
				RoleList = roleList,
				//    DepotList = DepotList
			};
			return View(model);
		}
		//[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(AdminViewModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					var user = new IdentityUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNo };
					var result = await _userManager.CreateAsync(user, model.Password);
					if (result.Succeeded)
					{
						if (!string.IsNullOrEmpty(model.RoleID))
						{
							await _userManager.AddToRoleAsync(user, model.RoleID);
						}
						await _signInManager.SignInAsync(user, isPersistent: false);
						return RedirectToAction(nameof(HomeController.Index), "Home");
					}
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			catch (Exception ex)
			{

			}
			return View(model);
		}

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgotPassword(AdminViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				user.EmailConfirmed = true;
				if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
				{
					// Don't reveal that the user does not exist or is not confirmed
					return RedirectToAction("ForgotPasswordConfirmation");
				}

				// Generate a password reset token
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);

				// Optionally, send an email with a password reset link
				//if (_emailSender != null)
				//{
				//    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = token }, protocol: HttpContext.Request.Scheme);
				//   // await _emailSender.SendEmailAsync(model.Email, "Reset Password", $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>.");
				//}
				return RedirectToAction("ForgotPasswordConfirmation");
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			// Display the reset password view with email and token
			var model = new AdminViewModel { Email = email, Token = token };
			return View(model);
		}


		public async Task<IActionResult> Signout()
		{
			var Result = _signInManager.SignOutAsync();
			return RedirectToAction("Login", "Account");

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetPassword(AdminViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user == null)
				{
					// Handle the case where the user is not found
					return RedirectToAction(nameof(HomeController.Index), "Home");
				}

				var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

				if (result.Succeeded)
				{
					// Redirect to a password reset confirmation page
					return RedirectToAction(nameof(HomeController.Index), "Home");
				}

				// If password reset fails, add errors to ModelState and redisplay the form
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(model);
		}
		public IActionResult ResetPasswordConfirmation()
		{
			// Display a confirmation message or page
			return View();
		}

	}
}
