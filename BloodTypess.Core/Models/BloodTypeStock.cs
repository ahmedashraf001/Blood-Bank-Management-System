using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.Models
{
	public class BloodTypeStock
	{
		public int Id { get; set; }
		public string Type { get; set; } // A+, B-, O+,...
		public int AvailableUnits { get; set; }
		public DateTime LastUpdated { get; set; }

		// Foreign Key  
		public int BloodTypeId { get; set; }

		// Navigation Properties
		public BloodType BloodType { get; set; }
	}
}
