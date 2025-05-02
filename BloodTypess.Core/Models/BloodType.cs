using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.Models
{
	public class BloodType
	{
		public int Id { get; set; }
		public string Type { get; set; }  // "A+", "B-", "O+" ,....




		// Navigation Properties

		    // each bloodType has (--many--) donors
		public ICollection<Donor> Donors { get; set; }


			// each bloodType has (--one--) stockRow , and this stock row may have many Bloodtype units
			// each stockRow related to (--one--) bloodType
		public BloodTypeStock BloodTypeStock { get; set; } 
	}
}
