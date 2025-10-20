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
			// make custom cancellation token ensures the call auto-cancels if it takes > 5 seconds.
			using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			cts.CancelAfter(TimeSpan.FromSeconds(5));
	

		    // create HTTP request to the API endpoint with full control over the request and response.
			var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}{BloodTypesMap.mp[bloodType]}");
			// HttpCompletionOption.ResponseHeadersRead: Return as soon as headers arrive, don’t buffer the entire body first.
			var resp = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cts.Token);
			// ensure the response is successful, otherwise throw an exception.
			resp.EnsureSuccessStatusCode();
            // Manually deserializes : more flexible (can use custom options, handle nulls, etc.).
			var model = await resp.Content.ReadFromJsonAsync<BloodTypeInfoDTO>(cancellationToken: cts.Token);
			// return with ?? Returns a default object if deserialization failed 
			return model ?? new BloodTypeInfoDTO();  

		}
	}
}
