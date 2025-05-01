using BloodTypess.Core.DTOs.UserDtos;
using BloodTypess.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Business.Interfaces
{
	public interface IUserService
	{
		Task<bool> LoginAsync(LoginDto model);
		Task<IdentityResult> RegisterAsync(AppUser user, string password);
		Task LogoutAsync();

	}

}
