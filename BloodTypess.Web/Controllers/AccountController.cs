using BloodTypess.Business.Interfaces;
 using BloodTypess.Core.DTOs.UserDtos;
using BloodTypess.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;

namespace BloodTypess.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserService _userService;

		public AccountController(IUserService userService)
		{
			_userService = userService;
		}


		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login() => View();

		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginDto model)
		{
			if (!ModelState.IsValid)
			{
				return View(model); // Return the model to show validation errors
			}

			var result = await _userService.LoginAsync(model);

			if (result)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
				return View(model);
			}
			return View(model);
		}



		[AllowAnonymous]
		[HttpGet]
		public IActionResult Register() => View();

		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterDto model)
		{

			if (!ModelState.IsValid)
				return View("Register", model);

			 
				var user = new AppUser
				{
					FirstName = model.FirstName,
					LastName = model.LastName,
					Email = model.Email,
					UserName = model.Email
				};

				var result = await _userService.RegisterAsync(user, model.Password);

				if (result.Succeeded)
				{

				  var LoginModel =  new LoginDto
					{
						Email = model.Email,
						Password = model.Password,
						RememberMe = false
					};

				    await _userService.LoginAsync(LoginModel);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
				
			

			return View("Register" , model);
		}



		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await _userService.LogoutAsync();
			return RedirectToAction("Index", "Home");
		}


		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}

