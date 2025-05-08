using BloodTypess.Core.DTOs;
using BloodTypess.Core.DTOs.ReportDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.DataAccess.Interfaces
{
	public interface IReportRepository
	{
 		Task<List<BloodTypeStockReportDto>> GetBloodStockReportAsync();

 		Task<List<EligibleDonorsByTypeDto>> GetEligibleDonorsByTypeAsync();

 		Task<List<AgeGroupDto>> GetDonorAgeGroupsAsync();

 		Task<List<RiskBloodTypeDto>> GetRiskyBloodTypesAsync();

		Task<List<string>> GetZeroStockBloodTypesAsync();

 
 	}
}
