using BloodTypess.Core.DTOs;
using BloodTypess.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BloodTypess.DataAccess;
namespace BloodTypess.DataAccess.Services
{

	public class BloodTypeApiService : IBloodTypeApiService
	{
		

		private HttpClient _httpClient;

		public BloodTypeApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		 
		 

		public Task<BloodTypeInfoDTO> GetBloodTypeInfoAsync(string bloodType)
		{

			var response = _httpClient.GetFromJsonAsync<BloodTypeInfoDTO>($"https://68103ecf27f2fdac2410ad0f.mockapi.io/blood/bloodtypes/{BloodTypesMap.mp[bloodType]}");
			return response;

		}
	}
}
