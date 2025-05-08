using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.DTOs.ReportDtos
{
	public class BloodTypeStockReportDto
	{
		public string Type { get; set; } // A+, B-, O+,...
		public int AvailableUnits { get; set; }
		public DateTime LastUpdated { get; set; }
	}
}
