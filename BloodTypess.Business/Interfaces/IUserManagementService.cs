using BloodTypess.Core.DTOs.UserManagementDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Business.Interfaces
{
	public interface IUserManagementService
    {
		Task<List<UserListViewDto>> GetAllUsersAsync();
		Task<UserDetailsViewDto> GetUserDetailsAsync(string id);
		Task<UserEditViewDto> GetUserForEditAsync(string id);
		Task<List<string>> GetAllRolesAsync();
		Task<IdentityResult> UpdateUserAsync(UserEditViewDto model);
		Task<IdentityResult> DeleteUserAsync(string id, string currentUserEmail);

	}
}
