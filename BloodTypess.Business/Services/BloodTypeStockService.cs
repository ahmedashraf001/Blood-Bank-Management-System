using Azure;
using BloodTypess.Business.Interfaces;
using BloodTypess.Core.DTOs;
using BloodTypess.Core.Models;
using BloodTypess.DataAccess.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Business.Services
{
	public class BloodTypeStockService : IBloodTypeStockService
	{
		private readonly IBloodTypeStockRepository _bloodTypeStockRepository;
		private readonly IBloodTypeService _BloodTypeService;
		private readonly IMemoryCache _cache;

		public BloodTypeStockService(IBloodTypeStockRepository bloodTypeStockRepository,
			IBloodTypeService bloodTypeService,
			IMemoryCache cache)
		{
			_bloodTypeStockRepository = bloodTypeStockRepository;
			_BloodTypeService = bloodTypeService;
			_cache = cache;
		}
		public async Task<IEnumerable<BloodTypeStockDto>> GetAllBloodTypesAsync()
		{
			const string cacheKey = "BloodTypeStockCache";

			// Try to get from cache
			if (_cache.TryGetValue(cacheKey, out IEnumerable<BloodTypeStockDto> cachedList))
			{
				return cachedList;
			}

 			var bloodTypes = await _bloodTypeStockRepository.GetAllAsync();
			var dtoList = bloodTypes.Select(MapToDto).ToList();

			// Store in cache
			var cacheEntryOptions = new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMinutes(10))
				.SetAbsoluteExpiration(TimeSpan.FromHours(1));

			_cache.Set(cacheKey, dtoList, cacheEntryOptions);

			return dtoList;
		}

		public async Task<BloodTypeStockDto> GetBloodTypeByIdAsync(int id)
		{
			var bloodType = await _bloodTypeStockRepository.GetByIdAsync(id);
			return bloodType != null ? MapToDto(bloodType) : null;
		}

		public async Task<BloodTypeStockDto> AddBloodTypeAsync(BloodTypeStockDto bloodTypestockDto)
		{
			var bloodTypestock = await MapToEntity(bloodTypestockDto);
			var addedBloodType = await _bloodTypeStockRepository.AddAsync(bloodTypestock);
			_cache.Remove("BloodTypeStockCache");
			return MapToDto(addedBloodType);
		}

		public async Task<BloodTypeStockDto> UpdateBloodTypeAsync(BloodTypeStockDto bloodTypestockDto)
		{
			var bloodTypestock = await MapToEntity(bloodTypestockDto);
			var updatedBloodType = await _bloodTypeStockRepository.UpdateAsync(bloodTypestock);
			_cache.Remove("BloodTypeStockCache");
			return MapToDto(updatedBloodType);
		}

		public async Task DeleteBloodTypeAsync(int id)
		{
			await _bloodTypeStockRepository.DeleteAsync(id);
			_cache.Remove("BloodTypeStockCache");
		}

		// map between DTOs and entities
		private BloodTypeStockDto MapToDto(BloodTypeStock bloodType)
		{
			return new BloodTypeStockDto
			{
				Id = bloodType.Id,
				Type = bloodType.Type,
				AvailableUnits = bloodType.AvailableUnits,
				LastUpdated = bloodType.LastUpdated,
				BloodTypeId = bloodType.BloodTypeId
			};
		}

		private async Task<BloodTypeStock> MapToEntity(BloodTypeStockDto dto)
		{
			var bloodType = await _BloodTypeService.GetBloodTypeByIdAsync(dto.BloodTypeId);
			return new BloodTypeStock
			{
				Id = dto.Id,
				Type = bloodType.Type,
				AvailableUnits = dto.AvailableUnits,
				LastUpdated = dto.LastUpdated,
				BloodTypeId = dto.BloodTypeId

			};
		}

		public bool IsBloodTypeExist(BloodTypeStockDto model)
		{

			var BloodTypeStock = _bloodTypeStockRepository.GetAllAsync().Result.FirstOrDefault(x => x.Type == model.Type);
			if (BloodTypeStock == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public void PurgeBloodTypeStockCache()
		{
			_cache.Remove("BloodTypeStockCache");
		}
	}
}
