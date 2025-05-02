using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.DTOs
{
	public class BloodTypeStockDto
	{
		public int Id { get; set; }
		public string? Type { get; set; }
		public int AvailableUnits { get; set; }
		public DateTime LastUpdated { get; set; }
		//FK
		public int BloodTypeId { get; set; }

	}
}
