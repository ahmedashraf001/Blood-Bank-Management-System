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
using BloodTypess.Core.Configurations;
using Microsoft.Extensions.Options;


namespace BloodTypess.DataAccess.Services
{

	public class BloodTypeApiService : IBloodTypeApiService
	{
		

		private HttpClient _httpClient;
		private string _baseUrl;
 
		public BloodTypeApiService(HttpClient httpClient , IOptions<BloodTypeApiOptions> BloodTypeApioptions)
		{
			_httpClient = httpClient;
			_baseUrl = BloodTypeApioptions.Value.BaseUrl;
		}

		 
		 

		public  async Task<BloodTypeInfoDTO> GetBloodTypeInfoAsync(string bloodType , CancellationToken cancellationToken)
		{
			var url = $"{_baseUrl}{BloodTypesMap.mp[bloodType]}";
			var response =  await _httpClient.GetFromJsonAsync<BloodTypeInfoDTO>(url , cancellationToken);
			return response;

		}
	}
}
