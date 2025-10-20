using BloodTypess.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.Business.Interfaces
{
	public interface IBloodTypeStockService
	{
		Task<IEnumerable<BloodTypeStockDto>> GetAllBloodTypesAsync();
		Task<string> GetBloodTypeByIdAsync(int id);
		Task<BloodTypeStockDto> GetBloodTypeStockByIdAsync(int id);
		Task<BloodTypeStockDto> AddBloodTypeAsync(BloodTypeStockDto bloodTypeDto);
		Task<BloodTypeStockDto> UpdateBloodTypeAsync(BloodTypeStockDto bloodTypeDto);
		Task DeleteBloodTypeAsync(int id);
		public Task<bool> IsBloodTypeExist(string type);
		void PurgeBloodTypeStockCache();


	}
}
