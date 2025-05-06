using BloodTypess.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Business.Interfaces
{
    public interface IDonorService
    {
        Task<IEnumerable<DonorDto>> GetAllDonorsAsync();
		Task<DonorDto> GetDonorByIdAsync(int id);
		Task<DonorDto> AddDonorAsync(DonorDto donorDto);
		Task<DonorDto> UpdateDonorAsync(DonorDto donorDto);
		Task DeleteDonorAsync(int id);

		Task UpdateIsEiligibleToDonateAsync();
		Task UpdateAgeAsync();
	}
}
