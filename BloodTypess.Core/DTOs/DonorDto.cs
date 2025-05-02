using BloodTypess.Core.Models;
using System;
using System.Collections.Generic;
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
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Gender { get; set; }
		public int BloodTypeId { get; set;}

 		public string? BloodTypeName { get; set; } 

		public DateTime LastDonationDate { get; set; }
		public bool IsEligibleToDonate { get; set; }
		public string Address { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }

		// Computed property for displaying full name
		public string FullName => $"{FirstName} {LastName}";

		// Age calculation for easier display
		public int Age => DateTime.Now.Year - DateOfBirth.Year - (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);

	}
}
