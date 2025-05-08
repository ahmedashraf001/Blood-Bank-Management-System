using BloodTypess.Core.DTOs;
using BloodTypess.Core.DTOs.ReportDtos;
using BloodTypess.DataAccess.DataContext;
using BloodTypess.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BloodTypess.DataAccess.Repositories
{
	public class ReportRepository : IReportRepository
	{
		private ApplicationDbContext _context;

		public ReportRepository(ApplicationDbContext context )
		{
			_context = context;	
		}

		public async Task<List<BloodTypeStockReportDto>> GetBloodStockReportAsync()
		{
			var query = _context.BloodTypesStock
			.AsNoTracking()  
			.Select(s => new BloodTypeStockReportDto
			{
				Type = s.Type,
				AvailableUnits = s.AvailableUnits,
				LastUpdated = s.LastUpdated
			});

			 
			return await query.ToListAsync();
		}

		public async Task<List<EligibleDonorsByTypeDto>> GetEligibleDonorsByTypeAsync()
		{
			var query = _context.Donors
			.Where(d => d.IsEligibleToDonate)
			.GroupBy(d => d.bloodType.Type)  
			.Select(g => new EligibleDonorsByTypeDto
			{
				BloodType = g.Key,
				count = g.Count()
			});

			return await query.AsNoTracking().ToListAsync();  
		}

		public async Task<List<AgeGroupDto>> GetDonorAgeGroupsAsync()
		{
			var query = _context.Donors
			.AsNoTracking()
			.GroupBy(d => d.Age / 10)
			.Select(g => new AgeGroupDto
			{
				AgeGroup = $"{g.Key * 10}-{(g.Key + 1) * 10 - 1}",
				Count = g.Count()
			});

			return await query.ToListAsync();
		}

		

		public async Task<List<RiskBloodTypeDto>> GetRiskyBloodTypesAsync()
		{
			var query = _context.BloodTypesStock
			.Include(bt => bt.BloodType) // eager load
				.ThenInclude(b => b.Donors) // eager load nested
			.AsNoTracking()
			.Select(bt => new RiskBloodTypeDto
			{
				BloodType = bt.Type,
				AvailableUnits = bt.AvailableUnits,
				EligibleDonors = bt.BloodType.Donors.Count(d => d.IsEligibleToDonate)
			});

			return await query.OrderBy(r => r.AvailableUnits).ToListAsync(); 
		} 
		public async Task<List<string>> GetZeroStockBloodTypesAsync()
		{
			return await _context.BloodTypesStock
				.AsNoTracking()
				.Where(s => s.AvailableUnits == 0)
				.Select(s => s.Type)
				.ToListAsync(); 
		}
	}
}
