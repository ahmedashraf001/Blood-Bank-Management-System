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
namespace BloodTypess.Business.Services
{
	public class BloodTypeService : IBloodTypeService
	{
		private IBloodTypeApiService _apiService;
		private IBloodTypeRepository _bloodTypeRepository;

		public BloodTypeService(IBloodTypeApiService apiService  , IBloodTypeRepository bloodTypeRepository)
		{
			_apiService = apiService;
			_bloodTypeRepository = bloodTypeRepository;
		}
		public async Task<BloodTypeInfoDTO> GetBloodTypeInfo(string bloodType)
		{
			var response = await _apiService.GetBloodTypeInfoAsync(bloodType);
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
