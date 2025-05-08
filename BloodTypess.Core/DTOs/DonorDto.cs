using BloodTypess.Core.Models;
using BloodTypess.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.DTOs
{
	public class DonorDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[EmailAddress(ErrorMessage = "Invalid email format")]
		public string Email { get; set; }

		[Phone(ErrorMessage = "Invalid phone number")]
		public string PhoneNumber { get; set; }

		[NotInFutureDateAttribute(ErrorMessage = "Date of birth cannot be in the future")]

		[AllowedAgeAttribute(ErrorMessage = "Donor must be at least 18 years old")]
		public DateTime DateOfBirth { get; set; }
		public string Gender { get; set; }
		public int BloodTypeId { get; set;}

 		public string? BloodTypeName { get; set; }

		[NotInFutureDateAttribute(ErrorMessage = "Date cannot be in the future")]
		public DateTime LastDonationDate { get; set; }
 		
		public string City { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }

		// Computed property for displaying full name
		public string FullName => $"{FirstName} {LastName}";

 		public int Age { get; set; }
		public bool IsEligibleToDonate { get; set; }

	}
}
