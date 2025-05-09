using BloodTypess.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.DTOs.UserManagementDtos
{
	public class UserEditViewDto
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
 		public string Role { get; set; }
		public bool EmailConfirmed { get; set; }

		// For password change (optional)

 
		[DataType(DataType.Password)]
		[OptionalPasswordAttribute]
 		public string? NewPassword { get; set; }
 
	}
}
