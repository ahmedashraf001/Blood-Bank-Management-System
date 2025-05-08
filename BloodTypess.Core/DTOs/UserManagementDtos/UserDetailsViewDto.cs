using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.DTOs.UserManagementDtos
{
	public class UserDetailsViewDto
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
 		public string Role { get; set; }
		public bool EmailConfirmed { get; set; }
	}
}
