using BloodTypess.Business.Interfaces;
using BloodTypess.Core.DTOs;
using BloodTypess.Core.Models;
using BloodTypess.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

		public DonorService(IDonorRepository donorRepository
			,IBloodTypeStockRepository bloodTypeStockRepository,
			IBloodTypeService BloodTypeService)
		{
			_donorRepository = donorRepository;
			_bloodTypeStockRepository = bloodTypeStockRepository;
			_bloodTypeService = BloodTypeService;
		}

		public async Task<IEnumerable<DonorDto>> GetAllDonorsAsync()
		{
			var donors = await _donorRepository.GetAllAsync();
			return donors.Select(MapToDto);
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
			return MapToDto(addedDonor);
		}

		public async Task<DonorDto> UpdateDonorAsync(DonorDto donorDto)
		{
			var donor = await MapToDonorEntityAsync(donorDto);
			var updatedDonor = await _donorRepository.UpdateAsync(donor);
			return MapToDto(updatedDonor);
		}

		public async Task DeleteDonorAsync(int id)
		{
			await _donorRepository.DeleteAsync(id);
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
		}
		public async Task UpdateAgeAsync()
		{
			await _donorRepository.UpdateAgeAsync();
		}
	}
}
