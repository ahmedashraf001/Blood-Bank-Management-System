using BloodTypess.Business.Interfaces;
using BloodTypess.Core.DTOs;
using BloodTypess.Core.DTOs.ReportDtos;
using BloodTypess.DataAccess.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BloodTypess.Business.Services
{
	public class ReportService : IReportService
	{
		private readonly IReportRepository _reportRepository;
		private readonly IMemoryCache _cache;

		public ReportService(IReportRepository reportRepository, IMemoryCache cache)
		{
			_reportRepository = reportRepository;
			_cache = cache;
		}
		public async Task<List<BloodTypeStockReportDto>> GetBloodStockReportAsync()
		{
			const string cacheKey = "BloodStockReport";
			if (!_cache.TryGetValue(cacheKey, out List<BloodTypeStockReportDto> cachedData))
			{
				cachedData = await _reportRepository.GetBloodStockReportAsync();

				_cache.Set(cacheKey, cachedData, new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromMinutes(10))
					.SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			return cachedData;
		}

		public async Task<List<AgeGroupDto>> GetDonorAgeGroupsAsync()
		{
			const string cacheKey = "DonorAgeGroups";
			if (!_cache.TryGetValue(cacheKey, out List<AgeGroupDto> cachedData))
			{
				cachedData = await _reportRepository.GetDonorAgeGroupsAsync();
				_cache.Set(cacheKey, cachedData, new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromMinutes(10))
					.SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			return cachedData;
		}

		public async Task<List<EligibleDonorsByTypeDto>> GetEligibleDonorsByTypeAsync()
		{
			const string cacheKey = "EligibleDonorsByType";
			if (!_cache.TryGetValue(cacheKey, out List<EligibleDonorsByTypeDto> cachedData))
			{
				cachedData = await _reportRepository.GetEligibleDonorsByTypeAsync();
				_cache.Set(cacheKey, cachedData, new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromMinutes(10))
					.SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			return cachedData;
		}

		public async Task<List<RiskBloodTypeDto>> GetRiskyBloodTypesAsync()
		{
			const string cacheKey = "RiskyBloodTypes";
			if (!_cache.TryGetValue(cacheKey, out List<RiskBloodTypeDto> cachedData))
			{
				cachedData = await _reportRepository.GetRiskyBloodTypesAsync();
				_cache.Set(cacheKey, cachedData, new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromMinutes(10))
					.SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			return cachedData;
		}

		public async Task<List<string>> GetZeroStockBloodTypesAsync()
		{
			const string cacheKey = "ZeroStockBloodTypes";
			if (!_cache.TryGetValue(cacheKey, out List<string> cachedData))
			{
				cachedData = await _reportRepository.GetZeroStockBloodTypesAsync();
				_cache.Set(cacheKey, cachedData, new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromMinutes(10))
					.SetAbsoluteExpiration(TimeSpan.FromHours(1)));
			}
			return cachedData;
		}
		public void PurgeReportCache()
		{
			_cache.Remove("BloodStockReport");
			_cache.Remove("DonorAgeGroups");
			_cache.Remove("EligibleDonorsByType");
			_cache.Remove("RiskyBloodTypes");
			_cache.Remove("ZeroStockBloodTypes");
		}
	}
}
