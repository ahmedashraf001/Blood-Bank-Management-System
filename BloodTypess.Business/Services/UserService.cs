using BloodTypess.Business.Interfaces;
using BloodTypess.Core.DTOs.UserDtos;
using BloodTypess.Core.Models;
using BloodTypess.DataAccess.DataContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Business.Services
{
	public class UserService : IUserService
	{
		private UserManager<AppUser> _userManager;
		private SignInManager<AppUser> _signInManager;

		public UserService(UserManager<AppUser> usermanager , SignInManager<AppUser> signInManager )
		{
			 _userManager = usermanager;
			_signInManager = signInManager;
		}
		public async Task<bool> LoginAsync(LoginDto model)
		{
			// to get the user Name for the UI
			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				// if the Claimas was added , remove it to avoid duplication and being up-to-date
				var existingClaims = await _userManager.GetClaimsAsync(user);
				var firstNameClaim = existingClaims.FirstOrDefault(c => c.Type == "FirstName");
				var lastNameClaim = existingClaims.FirstOrDefault(c => c.Type == "LastName");


				if (firstNameClaim != null)
					await _userManager.RemoveClaimAsync(user, firstNameClaim);

				if (lastNameClaim != null)
					await _userManager.RemoveClaimAsync(user, lastNameClaim);

				// Add the claims to the user
				var claims = new List<Claim>
					{
						new Claim("FirstName", user.FirstName ?? ""),
						new Claim("LastName", user.LastName ?? "")
					};

				// Add the claims to the user
				await _userManager.AddClaimsAsync(user, claims);

				// sign in (with Remeber me)
				await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);

				return true;
			}

			return false;

			// return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
		}

		public async Task<IdentityResult> RegisterAsync(AppUser user, string password)
		{
			
			var result = await _userManager.CreateAsync(user, password);
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "User");
			}
			return result;
		}

		public async Task LogoutAsync()
		{
			await _signInManager.SignOutAsync();
		}
	}
}
