using BloodTypess.Core.DTOs;
using BloodTypess.Core.DTOs.ReportDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Business.Interfaces
{
	public interface IReportService
	{
		// Show blood stock Units per type
		Task<List<BloodTypeStockReportDto>> GetBloodStockReportAsync();

		// show how many eligible donors exist for each blood type.
		Task<List<EligibleDonorsByTypeDto>> GetEligibleDonorsByTypeAsync(); 

		// Understand age ranges 
		Task<List<AgeGroupDto>> GetDonorAgeGroupsAsync();

		// show risk or opportunity , depending on Donors & BloodStock
		Task<List<RiskBloodTypeDto>> GetRiskyBloodTypesAsync();

		Task<List<string>> GetZeroStockBloodTypesAsync();
	     void PurgeReportCache();




	}
}
