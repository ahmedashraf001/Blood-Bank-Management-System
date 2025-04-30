using BloodTypess.Business.Interfaces;
using BloodTypess.Core.DTOs;
using BloodTypess.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodTypess.DataAccess;
namespace BloodTypess.Business.Services
{
	public class BloodTypeService : IBloodTypeService
	{
		private IBloodTypeApiService _apiService;

		public BloodTypeService(IBloodTypeApiService apiService )
		{
			_apiService = apiService;
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
	}
}
