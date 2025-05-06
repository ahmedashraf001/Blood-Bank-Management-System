using BloodTypess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.DataAccess.Interfaces
{
	public interface IDonorRepository
	{
		Task<IEnumerable<Donor>> GetAllAsync();
		Task<Donor> GetByIdAsync(int id);
		Task<Donor> AddAsync(Donor donor);
		Task<Donor> UpdateAsync(Donor donor);
		Task DeleteAsync(int id);


		Task UpdateAgeAsync();
		Task UpdateIsEiligibleToDonateAsync();
	}
}
