using BloodTypess.Business.Interfaces;
using BloodTypess.Core.DTOs;
using BloodTypess.Core.Models;
using BloodTypess.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Business.Services
{
	public class DonorService : IDonorService
	{
		private readonly IDonorRepository _donorRepository;
		private readonly IBloodTypeStockRepository _bloodTypeStockRepository;
		private readonly IBloodTypeService _bloodTypeService;
		private readonly IMemoryCache _cache;

		public DonorService(IDonorRepository donorRepository
			,IBloodTypeStockRepository bloodTypeStockRepository,
			IBloodTypeService BloodTypeService,
			IMemoryCache cache )
		{
			_donorRepository = donorRepository;
			_bloodTypeStockRepository = bloodTypeStockRepository;
			_bloodTypeService = BloodTypeService;
			_cache = cache;
		}

		public async Task<IEnumerable<DonorDto>> GetAllDonorsAsync()
		{
			var sw = Stopwatch.StartNew();

			const string cacheKey = "all_donors_cache";

			if (_cache.TryGetValue(cacheKey, out IEnumerable<DonorDto> cachedDonors))
			{
				return cachedDonors;
			}

			
			var donors = await _donorRepository.GetAllAsync();
			sw.Stop();
			Console.WriteLine($"await _donorRepository.GetAllAsync(); took {sw.ElapsedMilliseconds}ms");

 			var result = donors.Select(MapToDto);

			// Set cache options
			var cacheEntryOptions = new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMinutes(10))   
				.SetAbsoluteExpiration(TimeSpan.FromHours(1)); 

			_cache.Set(cacheKey, result, cacheEntryOptions);

			return result;
		}

		public async Task<DonorDto> GetDonorByIdAsync(int id)
		{
			var donor = await _donorRepository.GetByIdAsync(id);
			return donor != null ? MapToDto(donor) : null;
		}

		public async Task<DonorDto> AddDonorAsync(DonorDto donorDto)
		{
			var donor = await MapToDonorEntityAsync(donorDto);
			var addedDonor = await _donorRepository.AddAsync(donor);
			_cache.Remove("all_donors_cache");
			return MapToDto(addedDonor);
		}

		public async Task<DonorDto> UpdateDonorAsync(DonorDto donorDto)
		{
			var donor = await MapToDonorEntityAsync(donorDto);
			var updatedDonor = await _donorRepository.UpdateAsync(donor);
			_cache.Remove("all_donors_cache");
			return MapToDto(updatedDonor);
		}

		public async Task DeleteDonorAsync(int id)
		{
			await _donorRepository.DeleteAsync(id);
			_cache.Remove("all_donors_cache");
		}

		// Helper methods to map between DTOs and entities
		private DonorDto MapToDto(Donor donor)
		{
 			return new DonorDto
			{
				Id = donor.Id,
				FirstName = donor.FirstName,
				LastName = donor.LastName,
				Email = donor.Email,
				PhoneNumber = donor.PhoneNumber,
				DateOfBirth = donor.DateOfBirth,
				Gender = donor.Gender,
				BloodTypeId = donor.BloodTypeId,
				LastDonationDate = donor.LastDonationDate,
				IsEligibleToDonate = donor.IsEligibleToDonate,
				City = donor.City,
				CreatedDate = donor.CreatedDate,
				UpdatedDate = donor.UpdatedDate,
				BloodTypeName= donor.BloodTypeName,
				Age=donor.Age
				
 			};
		}

		private async Task<Donor> MapToDonorEntityAsync(DonorDto dto)
		{
			 var bloodType = await _bloodTypeService.GetBloodTypeByIdAsync(dto.BloodTypeId);

			return new Donor
			{
				Id = dto.Id,
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Email = dto.Email,
				PhoneNumber = dto.PhoneNumber,
				DateOfBirth = dto.DateOfBirth,
				Gender = dto.Gender,
 				LastDonationDate = dto.LastDonationDate,
				IsEligibleToDonate = dto.IsEligibleToDonate,
				City = dto.City,
				CreatedDate = dto.Id == 0 ? DateTime.Now : dto.CreatedDate,
				UpdatedDate = dto.Id == 0 ? null : DateTime.Now,
				BloodTypeId = dto.BloodTypeId,
				BloodTypeName = bloodType?.Type
 			};
		}

		public async Task UpdateIsEiligibleToDonateAsync()
		{
			await _donorRepository.UpdateIsEiligibleToDonateAsync();
			_cache.Remove("all_donors_cache");
		}
		public async Task UpdateAgeAsync()
		{
			await _donorRepository.UpdateAgeAsync();
			_cache.Remove("all_donors_cache");
		}

		public async Task PurgeDonorsCache()
		{
			_cache.Remove("all_donors_cache");
			await _donorRepository.UpdateIsEiligibleToDonateAsync();
			await _donorRepository.UpdateAgeAsync();
		}
	}
}
