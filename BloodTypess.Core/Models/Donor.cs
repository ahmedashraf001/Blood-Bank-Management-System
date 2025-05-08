using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.Models
{
	public class Donor
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Gender { get; set; }
 		public DateTime LastDonationDate { get; set; }
		public bool IsEligibleToDonate { get; set; }
		public string City { get; set; }
 
		 public DateTime CreatedDate { get; set; }
		 public DateTime? UpdatedDate { get; set; }
		public string? BloodTypeName { get; set; } // For displaying purposes
		public int Age { get; set; }

		// Foreigen Key
		public int BloodTypeId { get; set; }
		// Navigation properties
		public BloodType bloodType { get; set; }
	 

	}
}
