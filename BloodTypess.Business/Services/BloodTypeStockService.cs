using BloodTypess.Business.Interfaces;
using BloodTypess.Core.DTOs;
using BloodTypess.Core.Models;
using BloodTypess.DataAccess.Interfaces;
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

		public BloodTypeStockService(IBloodTypeStockRepository bloodTypeStockRepository,
			IBloodTypeService bloodTypeService)
		{
			_bloodTypeStockRepository = bloodTypeStockRepository;
			_BloodTypeService = bloodTypeService;
		}
		public async Task<IEnumerable<BloodTypeStockDto>> GetAllBloodTypesAsync()
		{
			var bloodTypes = await _bloodTypeStockRepository.GetAllAsync();
			return bloodTypes.Select(MapToDto);
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
			return MapToDto(addedBloodType);
		}

		public async Task<BloodTypeStockDto> UpdateBloodTypeAsync(BloodTypeStockDto bloodTypestockDto)
		{
			var bloodTypestock = await MapToEntity(bloodTypestockDto);
			var updatedBloodType = await _bloodTypeStockRepository.UpdateAsync(bloodTypestock);
			return MapToDto(updatedBloodType);
		}

		public async Task DeleteBloodTypeAsync(int id)
		{
			await _bloodTypeStockRepository.DeleteAsync(id);
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
	}
}
