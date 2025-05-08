using BloodTypess.Business.Interfaces;
using BloodTypess.Core.DTOs.UserManagementDtos;
using BloodTypess.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Business.Services
{
	public class UserManagementService : IUserManagementService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserManagementService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<List<UserListViewDto>> GetAllUsersAsync()
		{
			var users = await _userManager.Users.ToListAsync();

			var userDtos = new List<UserListViewDto>();

			foreach (var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				userDtos.Add(new UserListViewDto
				{
					Id = user.Id,
					Email = user.Email,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Role = roles.FirstOrDefault()
				});
			}

			return userDtos;
		}


		public async Task<UserDetailsViewDto> GetUserDetailsAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null) return null;

			var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

			return new UserDetailsViewDto
			{
				Id = user.Id,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Role = role,
				EmailConfirmed = user.EmailConfirmed
			};
		}

		public async Task<UserEditViewDto> GetUserForEditAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null) return null;

			var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

			return new UserEditViewDto
			{
				Id = user.Id,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Role = role,
				EmailConfirmed = user.EmailConfirmed
			};
		}

		public async Task<List<string>> GetAllRolesAsync()
		{
			return await _roleManager.Roles.Select(r => r.Name).ToListAsync();
		}

		public async Task<IdentityResult> UpdateUserAsync(UserEditViewDto model)
		{
			var user = await _userManager.FindByIdAsync(model.Id);
			if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });
			if (user.Email == "admin@blood.com")
				return IdentityResult.Failed(new IdentityError { Description = "Cannot change primary admin account details." });

			

			user.Email = model.Email;
			user.UserName = model.Email;
			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.EmailConfirmed = model.EmailConfirmed;
 
			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded) return result;


			var currentRoles = await _userManager.GetRolesAsync(user);
			// if the role is not changed, no need to update the roles
			if (!currentRoles.Contains(model.Role))
			{
				// Remove all existing roles
				var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
				if (!removeResult.Succeeded)
					return removeResult;
				// Add the new role
				var addResult = await _userManager.AddToRoleAsync(user, model.Role);
				if (!addResult.Succeeded)
					return addResult;

				if (!string.IsNullOrEmpty(model.NewPassword))
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);
					var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
					if (!passwordResult.Succeeded)
						return passwordResult;
				}
			}

			return IdentityResult.Success;
		}

		public async Task<IdentityResult> DeleteUserAsync(string id, string currentUserEmail)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });

			if (user.Email == "admin@blood.com")
				return IdentityResult.Failed(new IdentityError { Description = "Cannot delete the primary admin account." });

			// Prevent deleting the current user
			if (user.Email == currentUserEmail)
			{
				return IdentityResult.Failed(new IdentityError { Description = "You cannot delete your own account while logged in." });
			}

			return await _userManager.DeleteAsync(user);
		}
	}
}
