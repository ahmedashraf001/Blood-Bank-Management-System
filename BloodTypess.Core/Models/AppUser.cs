using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.Models
{
	public class AppUser : IdentityUser
	{
 		//public string Email { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
 	}
}
