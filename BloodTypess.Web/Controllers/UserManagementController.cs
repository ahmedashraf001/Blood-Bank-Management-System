using BloodTypess.Business.Interfaces;
using BloodTypess.Core.DTOs.UserManagementDtos;
using BloodTypess.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodTypess.Web.Controllers
{
 
	[Authorize(Roles = "Admin")]
	public class UserManagementController : Controller
	{
		private readonly IUserManagementService _userService;

		public UserManagementController(IUserManagementService userService)
		{
			_userService = userService;
		}

		// GET: /UserManagement
		public async Task<IActionResult> Index()
		{
			var usersListDto = await _userService.GetAllUsersAsync();
		     return View(usersListDto);
		}

		// GET: /UserManagement/Details/5
		public async Task<IActionResult> Details(string id)
		{
			var userDetailsDto = await _userService.GetUserDetailsAsync(id);
			if (userDetailsDto == null) return NotFound();
			return View(userDetailsDto);
		}

		// GET: /UserManagement/Edit/5
		public async Task<IActionResult> Edit(string id)
		{
			var userEditDto = await _userService.GetUserForEditAsync(id);
			if (userEditDto == null) return NotFound();

			// Get all available roles for the dropdown
			ViewBag.Roles = await _userService.GetAllRolesAsync();

			return View(userEditDto);
		}

		// POST: /UserManagement/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, UserEditViewDto model)
		{
			if (id != model.Id) return NotFound();

			// if the user submitted invalid data
			if (!ModelState.IsValid)
			{
				ViewBag.Roles = await _userService.GetAllRolesAsync();
				return View(model);
			}

			var result = await _userService.UpdateUserAsync(model);
			if (result.Succeeded) return RedirectToAction(nameof(Index));

			foreach (var error in result.Errors)
				ModelState.AddModelError(string.Empty, error.Description);

			// If something failed, Re display the form
			ViewBag.Roles = await _userService.GetAllRolesAsync();
			return View(model);
		}


		// GET: /UserManagement/Delete/5
		public async Task<IActionResult> Delete(string id)
		{
			var user = await _userService.GetUserDetailsAsync(id);
			if (user == null) return NotFound();
			return View(user);
		}

		// POST: /UserManagement/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var result = await _userService.DeleteUserAsync(id, User.Identity.Name);
			if (result.Succeeded)
				return RedirectToAction(nameof(Index));

			var user = await _userService.GetUserDetailsAsync(id);
			foreach (var error in result.Errors)
				ModelState.AddModelError(string.Empty, error.Description);

			return View("Delete", user);
		}

	}
}
