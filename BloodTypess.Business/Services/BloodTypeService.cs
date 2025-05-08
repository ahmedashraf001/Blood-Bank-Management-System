using BloodTypess.Business.Interfaces;
using BloodTypess.Core.DTOs;
using BloodTypess.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodTypess.DataAccess;
using BloodTypess.Core.Models;
using BloodTypess.DataAccess.Repositories;
using Microsoft.Extensions.Caching.Memory;
namespace BloodTypess.Business.Services
{
	public class BloodTypeService : IBloodTypeService
	{
		private IBloodTypeApiService _apiService;
		private IBloodTypeRepository _bloodTypeRepository;
		private IMemoryCache _cache;

		public BloodTypeService(IBloodTypeApiService apiService  
			, IBloodTypeRepository bloodTypeRepository,
			IMemoryCache cache)
		{
			_apiService = apiService;
			_bloodTypeRepository = bloodTypeRepository;
			_cache = cache;
		}
		public async Task<BloodTypeInfoDTO> GetBloodTypeInfo(string bloodType , CancellationToken cancellationToken)
		{
			string cacheKey = $"BloodTypeInfoCache_{bloodType}";

			// Try to get from cache
			if (_cache.TryGetValue(cacheKey, out BloodTypeInfoDTO cachedInfo))
			{
				return cachedInfo;
			}

			var response = await _apiService.GetBloodTypeInfoAsync(bloodType, cancellationToken);

			// Store in cache
			var cacheEntryOptions = new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMinutes(10))
				.SetAbsoluteExpiration(TimeSpan.FromHours(1));

			_cache.Set(cacheKey,response,cacheEntryOptions);

			return response;
		}

		public bool IsValidBloodType(string bloodType)
		{

			if(BloodTypesMap.mp.ContainsKey(bloodType))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public async Task<IEnumerable<BloodType>> GetAllBloodTypesAsync()
		{
			return await _bloodTypeRepository.GetAllAsync();
		}
		public async Task<BloodType> GetBloodTypeByIdAsync(int id)
		{
			var bloodType = await _bloodTypeRepository.GetByIdAsync(id);
			return bloodType;
		}

	}
}
