using BloodTypess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.DataAccess.Interfaces
{
	public interface IBloodTypeStockRepository
	{
		Task<IEnumerable<BloodTypeStock>> GetAllAsync();
		Task<BloodTypeStock> GetByIdAsync(int id);
		Task<BloodTypeStock> AddAsync(BloodTypeStock bloodType);
		Task<BloodTypeStock> UpdateAsync(BloodTypeStock bloodType);
		Task DeleteAsync(int id);
	}
}
