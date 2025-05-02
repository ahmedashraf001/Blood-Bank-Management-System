using BloodTypess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypess.DataAccess.Interfaces
{
	public interface IBloodTypeRepository
	{
		public  Task<IEnumerable<BloodType>> GetAllAsync();
		public   Task<BloodType> GetByIdAsync(int id);

	}
}
