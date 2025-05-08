using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Core.DTOs.ReportDtos
{
    public class DashboardViewDto
    {
		public List<BloodTypeStockReportDto> BloodStock { get; set; }
		public List<EligibleDonorsByTypeDto> EligibleDonors { get; set; }
		public List<AgeGroupDto> AgeGroups { get; set; }
		public List<RiskBloodTypeDto> RiskyBloodTypes { get; set; }
		public List<string> ZeroStockTypes { get; set; }
	}
}
