using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.DTOs.ReportDtos
{
	public class RiskBloodTypeDto
	{
		public string BloodType { get; set; }
		public int AvailableUnits { get; set; }
		public int EligibleDonors { get; set; }
	}
}
