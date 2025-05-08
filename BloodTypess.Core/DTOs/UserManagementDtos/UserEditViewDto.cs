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

		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).+$",
	     ErrorMessage = "Password must have at least one lowercase," +
			" one uppercase, one digit, and one special character.")]
		[DataType(DataType.Password)]
 		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
		public string ConfirmPassword { get; set; }
	}
}
